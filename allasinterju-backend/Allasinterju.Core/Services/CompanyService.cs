using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface ICompanyService{
    Task<DtoCompany> GetCompanyById(int id);
}
public class CompanyService : ICompanyService
{
    private readonly AllasinterjuContext _context;
    public CompanyService(AllasinterjuContext ctxt){
        _context = ctxt;
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