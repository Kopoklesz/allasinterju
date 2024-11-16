using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Allasinterju.Database.Models;
using System.Security.Claims;

public interface IJobService{
    Task AddJob(DtoJobAdd job, int id);
    Task<DtoJob> ById(int id);
    bool CompanyExists(int id);
    Task<List<DtoJobShort>> GetAllJobs();
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
}