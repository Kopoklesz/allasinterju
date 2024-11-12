using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface IUserService{
    Task<DtoUser> ById(int id);
    bool Exists(int id);
    Task<List<DtoJobShort>> GetAppliedJobs(int id);
}
public class UserService : IUserService
{
    private readonly AllasinterjuContext _context;
    public UserService(AllasinterjuContext ctxt){
        _context = ctxt;
    }
    public async Task<DtoUser> ById(int id)
    {
        return new DtoUser(
            await _context.Felhasznalos
                .Include(x => x.Kitoltottallas)
                .ThenInclude(x => x.Allas)
                .SingleAsync(x => x.Id==id)
        );
    }

    public bool Exists(int id)
    {
        return _context.Felhasznalos.Where(x => x.Id==id).Count()==1;
    }

    public async Task<List<DtoJobShort>> GetAppliedJobs(int id)
    {
        return await _context.Felhasznalos
                .Include(x => x.Kitoltottallas)
                .ThenInclude(x => x.Allas)
                .Where(x => x.Id==id)
                .SelectMany(x => x.Kitoltottallas)
                .Select(x => new DtoJobShort(x.Allas)).ToListAsync();
    }
}