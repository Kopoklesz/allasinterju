using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

public interface ICompetenceService
{
    Task AddToJob(string competence, int jobId, string level);
    Task AddToUser(string competence, int id, string level);
    Task DeleteForJob(int jobId, int id);
    Task DeleteForUser(int userId, int id);
    List<RDtoCompetence> GetAll();
    List<RDtoCompetence> GetForJob(int jobId);
    List<RDtoCompetence> GetForUser(int id);
}

public class CompetenceService : ICompetenceService
{
    private readonly AllasinterjuContext _context;
    public CompetenceService(AllasinterjuContext ctxt){
        _context = ctxt;
    }

    public async Task AddToJob(string competence, int jobId, string level)
    {
        var existing = await _context.Kompetencia
            .SingleOrDefaultAsync(x => x.Tipus.ToLower() == competence.ToLower());        
        if(existing==null){
            Kompetencium k = new Kompetencium{
                Tipus=competence
            };
            await _context.AddAsync(k);
            await _context.SaveChangesAsync();
            existing = await _context.Kompetencia
            .SingleOrDefaultAsync(x => x.Tipus.ToLower() == competence.ToLower());
        }
        Allaskompetencium fk = new Allaskompetencium{
            Allasid=jobId,
            Kompetencia=existing,
            Szint=level
        };
        await _context.SaveChangesAsync();
    }

    public async Task AddToUser(string competence, int id, string level)
    {
        var existing = await _context.Kompetencia
            .SingleOrDefaultAsync(x => x.Tipus.ToLower() == competence.ToLower());        
        if(existing==null){
            Kompetencium k = new Kompetencium{
                Tipus=competence
            };
            await _context.AddAsync(k);
            await _context.SaveChangesAsync();
            existing = await _context.Kompetencia
            .SingleOrDefaultAsync(x => x.Tipus.ToLower() == competence.ToLower());
        }
        Felhasznalokompetencium fk = new Felhasznalokompetencium{
            Felhasznaloid=id,
            Kompetencia=existing
        };
        await _context.SaveChangesAsync();
    }

    public async Task DeleteForJob(int jobId, int id)
    {
        var del = await _context.Allaskompetencia
            .SingleAsync(x => x.Kompetenciaid==id && x.Allasid==jobId);
        _context.Remove(del);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteForUser(int userId, int id)
    {
        var del = await _context.Felhasznalokompetencia
            .SingleAsync(x => x.Kompetenciaid==id && x.Felhasznaloid==userId);
        _context.Remove(del);
        await _context.SaveChangesAsync();
    }

    public List<RDtoCompetence> GetAll()
    {
        return _context.Kompetencia.ToList().ConvertAll(x => new RDtoCompetence(x));
    }

    public List<RDtoCompetence> GetForJob(int jobId)
    {
        return _context.Allaskompetencia
            .Include(x => x.Kompetencia)
            .Where(x => x.Allasid==jobId)
            .Select(x => x.Kompetencia)
            .ToList()
            .ConvertAll(x => new RDtoCompetence(x));
    }

    public List<RDtoCompetence> GetForUser(int id)
    {
        return _context.Felhasznalokompetencia
            .Include(x => x.Kompetencia)
            .Where(x => x.Felhasznaloid==id)
            .Select(x => x.Kompetencia)
            .ToList()
            .ConvertAll(x => new RDtoCompetence(x));
    }
}