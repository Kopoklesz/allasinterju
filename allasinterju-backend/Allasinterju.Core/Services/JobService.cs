using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Allasinterju.Database.Models;
using System.Security.Claims;
using Microsoft.Identity.Client;

public interface IJobService{
    Task AddJob(DtoJobAdd job, int id);
    void ApplyForJob(int jobId, int userId);
    Task<DtoJob> ById(int id);
    bool CompanyExists(int id);
    Task<List<DtoJobShort>> GetAllJobs();
    Task<RDtoKerdoiv> GetNextFreshRoundForUser(int allasId, int userId);
    Task<RDtoKerdoiv> GetRoundForCompany(int kerdoivId);
    Task SaveProgress(DtoSaveProgress sp, int userId, bool befejezve);
}
public class JobService : IJobService{
    private readonly AllasinterjuContext _context;
    public JobService(AllasinterjuContext ctxt){
        _context = ctxt;
    }

    public async Task AddJob(DtoJobAdd job, int id)
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
        await _context.AddAsync(a);
        await _context.SaveChangesAsync();
    }

    public async void ApplyForJob(int jobId, int userId)
    {
        Kitoltottalla ka = new Kitoltottalla{
            Allaskeresoid=userId,
            Allasid=jobId
        };
        await _context.AddAsync(ka);
        await _context.SaveChangesAsync();
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
        var currentRound = await _context.Kitoltottallas
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .Where(x => x.Allasid==allasId && x.Allaskeresoid==userId)
            .SelectMany(x => x.Kitoltottkerdoivs).Where(x => x.Befejezve==true).OrderBy(x => x.Kerdoiv.Kor).LastAsync();
        int kor = currentRound.Kerdoiv.Kor;
        var kerdoiv = await _context.Kerdoivs
            .Include(x => x.Kerdes)
            .ThenInclude(x => x.Valaszs)
            .Where(x => x.Kor>kor && x.Allasid==allasId)
            .OrderBy(x => x.Kor).FirstAsync();
        return new RDtoKerdoiv(kerdoiv, false);
    }

    public async Task<RDtoKerdoiv> GetRoundForCompany(int kerdoivId)
    {
        var kerdoiv = await _context.Kerdoivs
            .Include(x => x.Kerdes)
            .ThenInclude(x => x.Valaszs)
            .SingleAsync(x => x.Id==kerdoivId);
        return new RDtoKerdoiv(kerdoiv, true);
    }

    public async Task SaveProgress(DtoSaveProgress sp, int userId, bool befejezve)
    {
        var instance = await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .SingleAsync(x => x.Kerdoivid==sp.KerdoivId && x.Kitoltottallas.Allaskeresoid==userId)
            ?? new Kitoltottkerdoiv{
                Kerdoivid=sp.KerdoivId,
                Kitoltottallasid=_context.Kitoltottallas.First(x => x.Allaskeresoid==userId && x.Allasid==_context.Kerdoivs.Include(x => x.Allas).Single(x => x.Id==sp.KerdoivId).Allas.Id).Id,
                Befejezve=false
            };
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
        }
        await _context.SaveChangesAsync();
    }
}