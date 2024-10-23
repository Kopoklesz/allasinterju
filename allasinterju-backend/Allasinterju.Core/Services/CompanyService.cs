using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface ICompanyService{
    Task<List<DtoJobShort>> GetAdvertisedJobs(int id);
    Task<DtoCompany> GetCompanyById(int id);
}
public class CompanyService : ICompanyService
{
    private readonly AllasinterjuContext _context;
    public CompanyService(AllasinterjuContext ctxt){
        _context = ctxt;
    }

    public async Task<List<DtoJobShort>> GetAdvertisedJobs(int id)
    {
        var ceg = await _context.Cegs
            .Include(x => x.Allas)
            .ThenInclude(x => x.Telephely)
            .SingleAsync(x => x.Id == id);
        return ceg.Allas.Select(x => new DtoJobShort(x)).ToList();
    }

    public async Task<DtoCompany> GetCompanyById(int id)
    {
        return new DtoCompany(await _context.Cegs                
            .Include(x => x.Fotelephely)
            .Include(x => x.Allas)
                .ThenInclude(x => x.Telephely)           
            .SingleAsync(x => x.Id==id));
    }
}