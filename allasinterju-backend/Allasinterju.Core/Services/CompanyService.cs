using System.Data.SqlTypes;
using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface ICompanyService{
    Task CreateInvite(DtoInvitation invite, int companyId);
    void DeleteInvite(int id, int companyId);
    Task<List<DtoJobShort>> GetAdvertisedJobs(int id);
    List<RDtoInvitation> GetAllInvites(int companyId);
    Task<DtoCompany> GetCompanyById(int id);
}
public class CompanyService : ICompanyService
{
    private readonly AllasinterjuContext _context;
    public CompanyService(AllasinterjuContext ctxt){
        _context = ctxt;
    }

    public async Task CreateInvite(DtoInvitation invite, int companyId){
        Meghivokod m = new Meghivokod{
            Kod=invite.Code,
            Ervenyesseg=invite.Expiration!=null ? invite.Expiration : DateTime.Now.AddHours(8),
            Cegid=companyId
        };
        await _context.AddAsync(m);
        await _context.SaveChangesAsync();
    }

    public async void DeleteInvite(int id, int companyId)
    {
        var delete = await _context
            .Meghivokods
            .SingleOrDefaultAsync(x => x.Id==id && x.Cegid==companyId) ?? throw new Exception();
        _context.Remove(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<List<DtoJobShort>> GetAdvertisedJobs(int id)
    {
        var ceg = await _context.Cegs
            .Include(x => x.Allas)
            .SingleAsync(x => x.Id == id);
        return ceg.Allas.Select(x => new DtoJobShort(x)).ToList();
    }

    public List<RDtoInvitation> GetAllInvites(int companyId)
    {
        return _context.Meghivokods.Where(x => x.Cegid==companyId).ToList().ConvertAll(x => new RDtoInvitation(x));
    }

    public async Task<DtoCompany> GetCompanyById(int id)
    {
        return new DtoCompany(await _context.Cegs
            .Include(x => x.Allas)       
            .SingleAsync(x => x.Id==id));
    }
}