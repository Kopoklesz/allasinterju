using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface ICompanyService{
    Task CreateInvite(DtoInvitation invite, int companyId);
    void DeleteInvite(int id, int companyId);
    Task<string> GenerateRandomInviteCode(DateTime expiration, int companyId);
    Task<List<DtoJobShort>> GetAdvertisedJobs(int id);
    List<RDtoInvitation> GetAllInvites(int companyId);
    Task<DtoCompany> GetCompanyById(int id);
    Task<int> GetCompanyIdByWorkerId(int userId);
    Task<List<RWorker>> GetWorkers(int companyId);
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<RMunkakereso> JobSeekerReport(int jobSeekerId);
    Task<List<RMunkakeresoShort>> ListAllJobSeekers();
    Task<List<RNomination>> ListNominations(int companyId);
    Task Nominate(BInviteToApplication ita);
    Task RemoveWorker(int workerId, int companyId);
}
public class CompanyService : ICompanyService
{
    private readonly AllasinterjuContext _context;

    private readonly ILeetcodeClient _leetcode;
    public CompanyService(AllasinterjuContext ctxt, ILeetcodeClient ltcd){
        _context = ctxt;
        _leetcode = ltcd;
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

    public async Task<string> GenerateRandomInviteCode(DateTime expiration, int companyId)
    {
        string pool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var builder = new StringBuilder();
        var random = new Random();
        for(int i=0;i<16;i++){
            var c = pool[random.Next(0,pool.Length)];
            builder.Append(c);
        }
        string kod = builder.ToString();
        await CreateInvite(new DtoInvitation{
            Expiration=expiration,
            Code=kod
        },companyId);
        return kod;
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

    public async Task<int> GetCompanyIdByWorkerId(int userId)
    {
        var worker = await _context.Felhasznalos.SingleAsync(x => x.Id==userId);
        return (int)worker.Cegid;
    }

    public async Task<List<RWorker>> GetWorkers(int companyId)
    {
        var workers = await _context.Felhasznalos.Where(x => x.Cegid==companyId).ToListAsync();
        return workers.ConvertAll(x => new RWorker(x));
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

    public async Task<RMunkakereso> JobSeekerReport(int jobSeekerId)
    {
        var jobSeeker = await _context.Felhasznalos
            .Include(x => x.Dokumenta)
            .Include(x => x.Vegzettsegs)
            .Include(x => x.Kitoltottallas)
            .ThenInclude(x => x.Allas)
            .ThenInclude(x => x.Ceg)
            .Include(x => x.Kitoltottallas)
            .ThenInclude(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .SingleAsync(x => x.Id==jobSeekerId);
        RMunkakereso resp = new RMunkakereso(jobSeeker);
        try{
            resp.LeetcodeStatisztika = await _leetcode.GetUserStats(jobSeeker.Leetcode ?? "");
        }
        return resp;
    }

    public async Task<List<RMunkakeresoShort>> ListAllJobSeekers()
    {
        var people = await _context.Felhasznalos
            .Include(x => x.Felhasznalokompetencia)
            .ThenInclude(x => x.Kompetencia).ToListAsync();
        return people.ConvertAll(x => new RMunkakeresoShort(x));
    }

    public async Task<List<RNomination>> ListNominations(int companyId)
    {
        var noms = await _context.Ajanlas
            .Include(x => x.Allaskereso)
            .Include(x => x.Allas)
            .ThenInclude(x => x.Ceg)
            .Where(x => x.Allas.Cegid==companyId)
            .ToListAsync();
        return noms.ConvertAll(x => new RNomination(x));
    }

    public async Task Nominate(BInviteToApplication ita)
    {
        Ajanla a = new Ajanla{
            Allasid=ita.AllasId,
            Allaskeresoid=ita.MunkakeseroId,
            Jelentkezve=false
        };
        await _context.AddAsync(a);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveWorker(int workerId, int companyId)
    {
        var remove = await _context.Felhasznalos.SingleAsync(x => x.Id==workerId && x.Cegid==companyId) ?? throw new Exception();
        _context.Remove(remove);
        await _context.SaveChangesAsync();
    }
}