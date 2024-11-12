using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Allasinterju.Database.Models;

public interface IJobService{
    Task<DtoJob> ById(int id);
    Task<List<DtoJobShort>> GetAllJobs();
}
public class JobService : IJobService{
    private readonly AllasinterjuContext _context;
    public JobService(AllasinterjuContext ctxt){
        _context = ctxt;
    }

    public async Task<DtoJob> ById(int id)
    {
        return new DtoJob(await _context.Allas
            .Include(x => x.Ceg)
            .Include(x => x.Telephely)
            .SingleAsync(x => x.Id==id));
    }

    public async Task<List<DtoJobShort>> GetAllJobs()
    {
        return _context.Allas
            .Include(x => x.Ceg)
            .Include(x => x.Telephely)
            .ToList().ConvertAll(x => new DtoJobShort(x));
    }
}