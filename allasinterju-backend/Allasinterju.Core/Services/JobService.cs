using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Allasinterju.Database.Models;
using System.Security.Claims;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore.Query.Internal;

public interface IJobService{
    Task<int> AddJob(DtoJobAdd job, int id);
    Task<int> AddRound(DtoKerdoivLetrehozas klh);
    Task ApplyForJob(int jobId, int userId);
    Task ArrangeRounds(BRoundArrange ra);
    Task<DtoJob> ById(int id);
    bool CompanyExists(int id);
    Task DecideTovabbjutas(BTovabbjutas tov);
    Task EvaluateRoundAI(BEvalAI ea);
    Task<List<RApplicationShort>> GetAllApplications(int jobId);
    Task<List<DtoJobShort>> GetAllJobs();
    Task<double?> GetFinalGrade(BApplication appl);
    Task<int> GetJobId(int kitoltottKerdoivId);
    Task<RDtoKerdoiv> GetNextFreshRoundForUser(int allasId, int userId);
    Task<List<RMunkakeresoShort>> GetRecommendedJobSeekersForJob(int jobId);
    Task<RDtoKerdoiv> GetRoundForCompany(int kerdoivId);
    Task<List<RRound>> GetRounds(int jobId);
    List<RDtoKerdoivShort> GetRoundsShort(int jobId);
    Task<RRoundSummary> GetRoundSummary(int kerdoivId);
    Task<RApplication> GetSingleApplication(BApplication appl);
    Task GiveGrade(BGrading grade);
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<bool> IsWithinTimeFrame(int kerdoivId, int userId);
    Task SaveProgress(DtoSaveProgress sp, int userId, bool befejezve);
}
public class JobService : IJobService{
    private readonly AllasinterjuContext _context;
    private readonly IJudge0Client _judge0Client;
    private readonly IOpenAIClient _openAIClient;
    public JobService(AllasinterjuContext ctxt, IJudge0Client clnt, IOpenAIClient clnt2){
        _context = ctxt;
        _judge0Client = clnt;
        _openAIClient = clnt2;
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
        if(_context.Kitoltottallas.Where(x => x.Allasid==jobId && x.Allaskeresoid==userId).Count()!=0){
            return;
        }
        Kitoltottalla ka = new Kitoltottalla{
            Allaskeresoid=userId,
            Allasid=jobId,
            Kitolteskezdet=DateTime.Now
        };
        /*var userInstance = await _context.Felhasznalos.SingleAsync(x => x.Id==userId);
        userInstance.Kitoltottallas.Add(new Kitoltottalla{
            Allaskereso=userInstance,
            Allasid=jobId,
            Kitolteskezdet=DateTime.Now});*/
        _context.Add(ka);
            
        if(_context.Ajanlas.Where(x => x.Allasid==jobId && x.Allaskeresoid==userId).Any()){
            var a = await _context.Ajanlas.SingleAsync(x => x.Allasid==jobId && x.Allaskeresoid==userId);
            a.Jelentkezve=true;
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

    public async Task<RRoundSummary> GetRoundSummary(int kerdoivId)
    {
        return new RRoundSummary(await _context.Kerdoivs
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kitoltottallas)
            .ThenInclude(x => x.Allaskereso)
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

    public async Task EvaluateRoundAI(BEvalAI ea)
    {
        var instance=await _context.Kerdoivs.SingleAsync(x => x.Id==ea.KerdoivId);
        if(instance.Programming==true){
            EvalProg(ea);
        }
    }
    private async Task EvalProg(BEvalAI ea){
        string prompt="Generate a JSON array of objects with Id, Szazalek, and Tovabbjut fields. You will see a job listing that has been applied to by contestants. You will need to return "+ea.JeloltSzam+" people with a Tovabbjut value of true who are the most capable for the job. The others should get a false Tovabbjut field. You should also grade the applicants by the Szazalek field, which should be an integer between 0 and 100.\n";
        prompt+=JobInfoAI(ea.KerdoivId);
        
        var instance=await _context.Kerdoivs
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kitoltottallas)
            .ThenInclude(x => x.Allaskereso)
            .ThenInclude(x => x.Felhasznalokompetencia)
            .ThenInclude(x => x.Kompetencia)
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.KProgrammings)
            .ThenInclude(x => x.KProgrammingtestcases)
            .SingleAsync(x => x.Id==ea.KerdoivId);

        prompt+=ProgAI(instance.Programmings.First());
        foreach(var kitolto in instance.Kitoltottkerdoivs){
            prompt+=ProgUserAI(kitolto);
        }
        Console.WriteLine(prompt);
        var resp = await _openAIClient.RunPrompt(prompt);
        foreach(var elem in resp){
            try{
                var kk = await _context.Kitoltottkerdoivs.SingleAsync(x => x.Id==elem.Id);
                kk.Miajanlas=elem.Tovabbjut;
                kk.Miszazalek= (int?)elem.Szazalek;
                await _context.SaveChangesAsync();
            }
            catch{
                Console.WriteLine("Wrong ID");
            }
            
        }
    }
    private async Task<string> JobInfoAI(int kerdoivId){
        var instance=await _context.Allas.Include(x => x.Kerdoivs).
        Include(x => x.Allaskompetencia).ThenInclude(x => x.Kompetencia).SingleAsync(x => x.Kerdoivs.Any(y => y.Id==kerdoivId));
        string prompt="Job title: "+instance.Cim+"\n";
        prompt+="Description: "+instance.Leiras+"\n";
        prompt+="Role: "+instance.Munkakor+"\n";
        prompt+="Competencies: ";
        foreach(var komp in instance.Allaskompetencia){
            prompt+=komp.Kompetencia+" "+komp.Szint+" ";
        }
        return prompt+"\n";
        
    }
    private async Task<string> ProgAI(Programming p){
        string prompt="Question title: "+p.Title+"\n";
        prompt+="Description: "+p.Description+"\n";
        prompt+="Language: "+p.Language+"\n";
        return prompt+"\n";
    }
    private async Task<string> ProgUserAI(Kitoltottkerdoiv kk){
        string prompt = "Applicant Id "+kk.Id+":\n";
        prompt+="Competencies: ";
        foreach(var komp in kk.Kitoltottallas.Allaskereso.Felhasznalokompetencia){
            prompt+=komp.Kompetencia+" "+komp.Szint+" ";
        }
        prompt+="\n";
        prompt+="Given answer: "+kk.KProgrammings.First().Programkod;
        return prompt+"\n\n";
    }

    public async Task DecideTovabbjutas(BTovabbjutas tov)
    {
        var instance = await _context.Kitoltottkerdoivs.SingleAsync(x => x.Id==tov.KitoltottKerdoivId);
        instance.Tovabbjut=tov.Tovabbjut;
        await _context.SaveChangesAsync();
    }

    public async Task<List<RApplicationShort>> GetAllApplications(int jobId)
    {
        var jobInstance = await _context.Allas
            .Include(x => x.Kitoltottallas)
            .ThenInclude(x => x.Allaskereso)
            .Include(x => x.Kitoltottallas)
            .ThenInclude(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .SingleAsync(x => x.Id==jobId);
        List<RApplicationShort> resp = new List<RApplicationShort>();
        foreach(var ka in jobInstance.Kitoltottallas){
            resp.Add(new RApplicationShort(ka));
        }
        return resp;
    }

    public async Task<RApplication> GetSingleApplication(BApplication appl)
    {
        var ka = await _context.Kitoltottallas
            .Include(x => x.Allaskereso)
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .SingleAsync(x => x.Allasid==appl.JobId && x.Allaskeresoid==appl.MunkakeresoId);
        return new RApplication(ka);
    }

    public async Task<double?> GetFinalGrade(BApplication appl)
    {
        var ka = await _context.Kitoltottallas.SingleAsync(x => x.Allasid==appl.JobId && x.Allaskeresoid==appl.MunkakeresoId);
        if(ka.Kitoltottkerdoivs.Count() == ka.Allas.Kerdoivs.Count()){
            return ka.Kitoltottkerdoivs.Select(x => x.Szazalek).Average();
        }
        return null;
    }
}