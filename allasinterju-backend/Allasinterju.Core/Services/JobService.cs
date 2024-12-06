using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Allasinterju.Database.Models;
using System.Security.Claims;
using Microsoft.Identity.Client;

public interface IJobService{
    Task<int> AddJob(DtoJobAdd job, int id);
    Task<int> AddRound(DtoKerdoivLetrehozas klh);
    Task ApplyForJob(int jobId, int userId);
    Task ArrangeRounds(BRoundArrange ra);
    Task<DtoJob> ById(int id);
    bool CompanyExists(int id);
    Task<List<DtoJobShort>> GetAllJobs();
    Task<int> GetJobId(int kitoltottKerdoivId);
    Task<RDtoKerdoiv> GetNextFreshRoundForUser(int allasId, int userId);
    Task<List<RMunkakeresoShort>> GetRecommendedJobSeekersForJob(int jobId);
    Task<RDtoKerdoiv> GetRoundForCompany(int kerdoivId);
    Task<List<RRound>> GetRounds(int jobId);
    List<RDtoKerdoivShort> GetRoundsShort(int jobId);
    Task<RDtoRoundSummary> GetRoundSummary(int kerdoivId);
    Task GiveGrade(BGrading grade);
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<bool> IsWithinTimeFrame(int kerdoivId, int userId);
    // object? RunCode(int kitoltottKerdoivId);
    Task SaveProgress(DtoSaveProgress sp, int userId, bool befejezve);
}
public class JobService : IJobService{
    private readonly AllasinterjuContext _context;
    private readonly IJudge0Client _judge0Client;
    public JobService(AllasinterjuContext ctxt, IJudge0Client clnt){
        _context = ctxt;
        _judge0Client = clnt;
    }

    private async Task PassCodeToRun(Kitoltottkerde kk){
        var tesztesetek = kk.Kerdes.Tesztesets;
        foreach(var eset in tesztesetek){
            Lefutottteszteset lt = new Lefutottteszteset{
                Tesztesetid=eset.Id,
                Kitoltottkerdesid=kk.Id,
                Token=await _judge0Client.GiveCodeToRun(kk.Szovegesvalasz, eset.Bemenet, kk.Kerdes.Programnyelv)
            };
            Console.WriteLine("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
            await _context.AddAsync(lt);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<int> AddJob(DtoJobAdd job, int id)
    {        
        Alla a = new Alla{
            Cim=job.JobTitle,
            Munkakor=job.JobType,
            Munkarend=job.WorkOrder,
            Leiras=job.Description,
            Rovidleiras=job.ShortDescription,
            Hatarido=job.Deadline,
            Telephelyszoveg=job.Location,
            Cegid=id
        };
        foreach(var comp in job.Competences){
            var compcount = _context.Kompetencia.Where(x => x.Tipus==comp.Type).Count();
            if(compcount==1){
                var compinstance = await _context.Kompetencia.SingleAsync(x => x.Tipus==comp.Type);
                a.Allaskompetencia.Add(new Allaskompetencium{
                    Kompetencia=compinstance,
                    Szint=comp.Level
                });
            }
            else{
                Kompetencium ko = new Kompetencium{
                    Tipus=comp.Type
                };
                await _context.AddAsync(ko);
                a.Allaskompetencia.Add(new Allaskompetencium{
                        Kompetencia=ko,
                        Szint=comp.Level
                    });
            }
        }
        await _context.AddAsync(a);
        await _context.SaveChangesAsync();
        return a.Id;
    }

    public async Task<int> AddRound(DtoKerdoivLetrehozas klh)
    {
        Kerdoiv k = new Kerdoiv{
            Kor=klh.Kor,
            Nev=klh.Nev,
            Allasid=klh.AllasId,
            Kitoltesperc=klh.KitoltesPerc
        };
        foreach(var kerdes in klh.Kerdesek){
            Kerde ke = new Kerde{
                Szoveg=kerdes.Szoveg,
                Programalapszoveg=kerdes.ProgramozosAlapszoveg,
                Programozos=kerdes.Program,
                Kifejtos=kerdes.Kifejtos,
                Feleletvalasztos=kerdes.Valasztos,
                Programnyelv=kerdes.Programnyelv
            };
            if(kerdes.Tesztesetek!=null){
                foreach(var te in kerdes.Tesztesetek){
                    Teszteset t = new Teszteset{
                        Bemenet=te.Bemenet,
                        Kimenet=te.Kimenet
                    };
                    ke.Tesztesets.Add(t);
                }
            }
            if(kerdes.Valaszok!=null){
                foreach(var va in kerdes.Valaszok){
                    Valasz v = new Valasz{
                        Szoveg=va.ValaszSzoveg,
                        Helyes=va.Helyes
                    };
                    ke.Valaszs.Add(v);
                }
            }
            
            k.Kerdes.Add(ke);
        }
        await _context.AddAsync(k);
        await _context.SaveChangesAsync();
        return k.Id;
    }

    public async Task ApplyForJob(int jobId, int userId)
    {
        Console.WriteLine(jobId+" "+userId);
        Kitoltottalla ka = new Kitoltottalla{
            Allaskeresoid=userId,
            Allasid=jobId,
            Kitolteskezdet=DateTime.Now
        };
        await _context.AddAsync(ka);
        try{
            var a = await _context.Ajanlas.SingleAsync(x => x.Allasid==jobId && x.Allaskeresoid==userId);
            if(a!=null){
                a.Jelentkezve=true;
            }
        }
        catch{

        }
        _context.SaveChanges(); // MARS esetén nem lehet SaveChangesAsync-ot használni :(
    }

    public async Task<DtoJob> ById(int id)
    {
        return new DtoJob(await _context.Allas
            .Include(x => x.Ceg)
            .SingleAsync(x => x.Id==id));
    }

    public bool CompanyExists(int id)
    {
        return _context.Cegs.Where(x => x.Id==id).Count()==1;
    }

    public async Task<List<DtoJobShort>> GetAllJobs()
    {
        return _context.Allas
            .Include(x => x.Ceg)
            .ToList().ConvertAll(x => new DtoJobShort(x));
    }

    public async Task<RDtoKerdoiv> GetNextFreshRoundForUser(int allasId, int userId)
    {
        var currentRound = _context.Kitoltottallas
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .Where(x => x.Allasid==allasId && x.Allaskeresoid==userId)
            .SelectMany(x => x.Kitoltottkerdoivs).Where(x => x.Befejezve==true);
        Kerdoiv kerdoiv;
        if(currentRound.Any()){
            int kor = currentRound.OrderBy(x => x.Kerdoiv.Kor).Last().Kerdoiv.Kor;
            kerdoiv = await _context.Kerdoivs
            .Include(x => x.Kerdes)
            .ThenInclude(x => x.Valaszs)
            .Where(x => x.Kor>kor && x.Allasid==allasId)
            .OrderBy(x => x.Kor).FirstAsync(); 
        }
        else{
            kerdoiv = await _context.Kerdoivs
            .Include(x => x.Kerdes)
            .ThenInclude(x => x.Valaszs)
            .Where(x => x.Allasid==allasId)
            .OrderBy(x => x.Kor).FirstAsync(); 
        }
        var ka = await _context.Kitoltottallas.SingleAsync(x => x.Allasid==allasId && x.Allaskeresoid==userId);
        Kitoltottkerdoiv kk = new Kitoltottkerdoiv{
            Kitoltottallasid=ka.Id,
            Kerdoivid=kerdoiv.Id,
            Kitolteskezdet=DateTime.Now
        };
        await _context.AddAsync(kk);
        await _context.SaveChangesAsync();
        return new RDtoKerdoiv(kerdoiv, false);
    }

    public async Task<RDtoKerdoiv> GetRoundForCompany(int kerdoivId)
    {
        var kerdoiv = await _context.Kerdoivs
            .Include(x => x.Kerdes)
            .ThenInclude(x => x.Valaszs)
            .Include(x => x.Kerdes)
            .ThenInclude(x => x.Tesztesets)
            .SingleAsync(x => x.Id==kerdoivId);
        return new RDtoKerdoiv(kerdoiv, true);
    }

    public List<RDtoKerdoivShort> GetRoundsShort(int jobId)
    {
        var allas =  _context.Allas.Include(x => x.Kerdoivs).ThenInclude(x => x.Kerdes).Single(x => x.Id==jobId);
        return allas.Kerdoivs.ToList().ConvertAll(x => new RDtoKerdoivShort(x));
    }

    public async Task<RDtoRoundSummary> GetRoundSummary(int kerdoivId)
    {
        return new RDtoRoundSummary(await _context.Kerdoivs
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kitoltottallas)
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kitoltottkerdes)
            .ThenInclude(x => x.Valasztos)
            .SingleAsync(x => x.Id==kerdoivId));
    }

    public async Task<bool> HasAuthority(int allasId, int userId, bool isCompany)
    {
        var allas = await _context.Allas.SingleAsync(x => x.Id==allasId);
        var cegid = allas.Cegid;
        if(isCompany){
            return userId==cegid;
        }
        var felh = await _context.Felhasznalos.SingleAsync(x => x.Id==userId);
        return felh.Cegid==cegid;
    }

    public async Task<bool> IsWithinTimeFrame(int kerdoivId, int userId)
    {
        var kk = await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .Include(x => x.Kerdoiv)
            .SingleAsync(x => x.Kerdoivid==kerdoivId && x.Kitoltottallas.Allaskeresoid==userId);
        if(kk.Kerdoiv.Kitoltesperc!=null){
            DateTime vegleges = ((DateTime)kk.Kitolteskezdet).AddMinutes((double)kk.Kerdoiv.Kitoltesperc);
            // Console.WriteLine(vegleges);
            return DateTime.Now < vegleges;
        }
        return true;
    }

    public async Task SaveProgress(DtoSaveProgress sp, int userId, bool befejezve)
    {
        var instance = await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .SingleAsync(x => x.Kerdoivid==sp.KerdoivId && x.Kitoltottallas.Allaskeresoid==userId)
            ?? new Kitoltottkerdoiv{
                Kerdoivid=sp.KerdoivId,
                Kitoltottallasid=_context.Kitoltottallas.First(x => x.Allaskeresoid==userId && x.Allasid==_context.Kerdoivs.Include(x => x.Allas).Single(x => x.Id==sp.KerdoivId).Allas.Id).Id,
                Befejezve=befejezve
            };
        instance.Befejezve=befejezve;
        if(!_context.Kitoltottkerdoivs.Contains(instance)){
            await _context.AddAsync(instance);
            await _context.SaveChangesAsync();
        }

        var torlendo = _context.Kitoltottkerdes.Include(x => x.Kitoltottkerdoiv).ThenInclude(x => x.Kitoltottallas).Where(x => x.Kitoltottkerdoiv.Kitoltottallas.Allaskeresoid==userId && x.Kitoltottkerdoiv.Kerdoivid==sp.KerdoivId);
        // DELETE BEHAVIOUR MÓDOSÍTÁS ELENGEDHETETLEN LESZ
        _context.RemoveRange(torlendo);
        await _context.SaveChangesAsync();

        foreach(var kerdes in sp.Kerdesek){
            Kitoltottkerde kk = new Kitoltottkerde{
                Kitoltottkerdoivid=instance.Id,
                Kerdesid=kerdes.KerdesId,                
            };
            if(kerdes.Kifejtos==true){
                kk.Szovegesvalasz=kerdes.KifejtosValasz;
            }
            if(kerdes.Program==true){
                kk.Szovegesvalasz=kerdes.ProgramValasz;                
            }
            if(kerdes.Kivalasztos==true){
                kk.Valasztosid=kerdes.KivalasztottValaszId;
                var pont = _context.Valaszs.Single(x => x.Kerdesid==kerdes.KerdesId && x.Id==kerdes.KivalasztottValaszId).Pontszam;
                kk.Elertpont = pont;
            }
            await _context.AddAsync(kk);
            await _context.SaveChangesAsync();
            if(befejezve){
                try{
                    var ujkk = await _context.Kitoltottkerdes.Include(x => x.Kerdes).ThenInclude(x => x.Tesztesets).SingleAsync(x => x.Id==kk.Id);
                    await PassCodeToRun(ujkk);
                }
                catch{
                    
                }
            }
        }
        await _context.SaveChangesAsync();        
    }

    public async Task<List<RRound>> GetRounds(int jobId)
    {
        var rounds = await _context.Kerdoivs.Where(x => x.Allasid==jobId).ToListAsync();
        return rounds.ConvertAll(x => new RRound(x));
    }

    public async Task<List<RMunkakeresoShort>> GetRecommendedJobSeekersForJob(int jobId)
    {
        var wantedCompetencies = await _context.Allas
            .Include(x => x.Allaskompetencia)
            .ThenInclude(x => x.Kompetencia)
            .SelectMany(x => x.Allaskompetencia.Select(y => y.Kompetencia)).ToListAsync();
        var users = await _context.Felhasznalos
            .Include(x => x.Felhasznalokompetencia)
            .ThenInclude(x => x.Kompetencia)
            .Where(x => x.Felhasznalokompetencia.Any(y => wantedCompetencies.Contains(y.Kompetencia)))
            .ToListAsync();
        return users.ConvertAll(x => new RMunkakeresoShort(x));
    }

    public async Task<int> GetJobId(int kitoltottKerdoivId)
    {
        var kk = await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .SingleAsync(x => x.Id==kitoltottKerdoivId);
        return kk.Kitoltottallas.Allasid;
    }

    public async Task GiveGrade(BGrading grade)
    {
        var instance = await _context.Kitoltottkerdoivs.SingleAsync(x => x.Id==grade.KitoltottKerdoivId);
        instance.Szazalek=grade.Szazalek;
        await _context.SaveChangesAsync();
    }

    public async Task ArrangeRounds(BRoundArrange ra)
    {
        foreach(var elem in ra.Kerdoivek){
            var k = await _context.Kerdoivs.SingleAsync(x => x.Id==elem.KerdoivId);
            if(k.Allasid!=ra.JobId){
                throw new Exception();
            }
        }
        foreach(var elem in ra.Kerdoivek){
            var k = await _context.Kerdoivs.SingleAsync(x => x.Id==elem.KerdoivId);
            k.Kor=elem.Kor;
        }
        await _context.SaveChangesAsync();
        return;
    }
}