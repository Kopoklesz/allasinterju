using System.Security.Cryptography;
using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

public interface IProgrammingService
{
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<int> Add(BProgrammingAdd pa);
    Task Modify(BProgrammingModify pm);
    Task<bool> IsWithinTimeFrame(int kerdoivId, int userId, int minExtra=0);
    Task<RSolveP> Solve(int kerdoivId, int userId);
    Task<bool> IsSolvable(int kerdoivId, int userId, int minExtra=0);
    Task Sanitize();
    Task SaveProgress(BSaveProgressP sp, int userId, bool finish);
    Task<RKitoltottP> ViewSolved(int kitoltottKerdoivId);
    Task<RKitoltottP> ViewSolvedAsUser(int kitoltottKerdoivId, int userId);
}
public class ProgrammingService : IProgrammingService{
    private readonly AllasinterjuContext _context;
    public ProgrammingService(AllasinterjuContext ctxt){
        _context=ctxt;
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
    public async Task Sanitize(){
        var kk = _context.Kitoltottkerdoivs
            .Include(x => x.Kerdoiv)
            .Where(x => x.Befejezve==false && ((DateTime)x.Kitolteskezdet).AddMinutes((double)x.Kerdoiv.Kitoltesperc).AddMinutes(2)<DateTime.Now);
        foreach(var elem in kk){
            elem.Befejezve=true;
        }
        await _context.SaveChangesAsync();
    }
    public async Task<int> Add(BProgrammingAdd pa){
        Kerdoiv k = new Kerdoiv{
            Nev=pa.Name,
            Kor=pa.Round,
            Allasid=pa.JobId,
            Kitoltesperc=pa.TimeLimit,
            Programming=true,
            Design=false,
            Algorithm=false,
            Testing=false,
            Devops=false
        };
        Programming p = new Programming{
            Kerdoiv=k,
            Title=pa.Title,
            Description=pa.Description,
            Language=pa.Language,
            Codetemplate=pa.CodeTemplate
        };
        if(pa.TestCases!=null){
            foreach(var tc in pa.TestCases){
                p.Programmingtestcases.Add(new Programmingtestcase{
                    Input=tc.Input,
                    Output=tc.ExpectedOutput
                });
            }
        }
        await _context.AddRangeAsync(p, k);
        await _context.SaveChangesAsync();
        return k.Id;
    }

    public async Task Modify(BProgrammingModify pm)
    {
        var instance = await _context.Kerdoivs.SingleAsync(x => x.Id==pm.KerdoivId);
        instance.Nev=pm.Name;            
        instance.Kitoltesperc=pm.TimeLimit;
        instance.Programmings.First().Title=pm.Title;
        instance.Programmings.First().Description=pm.Description;
        instance.Programmings.First().Language=pm.Language;
        instance.Programmings.First().Codetemplate=pm.CodeTemplate;
        instance.Programmings.First().Programmingtestcases.Clear();
        if(pm.TestCases!=null){
            foreach(var tc in pm.TestCases){
                instance.Programmings.First().Programmingtestcases.Add(new Programmingtestcase{
                    Input=tc.Input,
                    Output=tc.ExpectedOutput
                });
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsWithinTimeFrame(int kerdoivId, int userId, int minExtra=0)
    {
        var kk = await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .Include(x => x.Kerdoiv)
            .SingleAsync(x => x.Kerdoivid==kerdoivId && x.Kitoltottallas.Allaskeresoid==userId);
        if(kk.Kerdoiv.Kitoltesperc!=null){
            DateTime vegleges = ((DateTime)kk.Kitolteskezdet).AddMinutes((double)kk.Kerdoiv.Kitoltesperc).AddMinutes(minExtra);
            // Console.WriteLine(vegleges);
            return DateTime.Now < vegleges;
        }
        return true;
    }

    public async Task<RSolveP> Solve(int kerdoivId, int userId)
    {
        var prog = await _context.Programmings
            .Include(x => x.Kerdoiv)
            .Include(x => x.KProgrammings)
            .ThenInclude(x => x.Kitoltottkerdoiv)
            .SingleAsync(x => x.Kerdoivid==kerdoivId);
        if(prog.KProgrammings.Count()==0){
            var ka = await _context.Kitoltottallas.SingleAsync(x => x.Allaskeresoid==userId);
            Kitoltottkerdoiv kk = new Kitoltottkerdoiv{
                Kerdoivid=kerdoivId,
                Kitoltottallas=ka,
                Befejezve=false,
                Kitolteskezdet=DateTime.Now
            };
            await _context.AddAsync(kk);
            await _context.SaveChangesAsync();
            return new RSolveP(prog);
        }
        var kp = prog.KProgrammings.First();
        return new RSolveP(kp);
    }

    public async Task<bool> IsSolvable(int kerdoivId, int userId, int minExtra=0)
    {
        var k = await _context.Kerdoivs.SingleAsync(x => x.Id==kerdoivId);
        var allasid = k.Allasid;
        var kkerdoivek = _context.Kitoltottallas
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .Single(x => x.Allaskeresoid==userId && x.Allasid==allasid)
            .Kitoltottkerdoivs;
        if(kkerdoivek.Count()==0){
            return await IsWithinTimeFrame(kerdoivId, userId, minExtra);
        }
        var rendezett = kkerdoivek.OrderBy(x => x.Kerdoiv.Kor);
        //bool tovabb = true;
        foreach(var elem in rendezett){
            if(elem.Kerdoivid==kerdoivId){
                break;
            }
            else if(elem.Tovabbjut==false || elem.Befejezve==false){
                return false;
            }
        }
        return await IsWithinTimeFrame(kerdoivId, userId, minExtra);
    }

    public async Task SaveProgress(BSaveProgressP sp, int userId, bool finish)
    {
        var allas = await _context.Allas.Include(x => x.Kerdoivs).SingleAsync(x => x.Kerdoivs.Any(y => y.Id==sp.KerdoivId));
        var ka = await _context.Kitoltottallas.SingleAsync(x => x.Allasid==allas.Id);
        var kk = await _context.Kitoltottkerdoivs.SingleAsync(x => x.Kerdoivid==sp.KerdoivId && x.Kitoltottallasid==ka.Id);
        var proginstance = await _context.Programmings.SingleAsync(x => x.Kerdoivid==sp.KerdoivId);
        if(kk.KProgrammings.Count()==0){
            kk.KProgrammings.Add(new KProgramming{
                Programkod=sp.Programkod,
                Programming=proginstance
            });            
        }
        else{
            kk.KProgrammings.First().Programkod=sp.Programkod;
        }
        if(finish){
            kk.Befejezve=true;
        }
        await _context.SaveChangesAsync();
    }

    public async Task<RKitoltottP> ViewSolved(int kitoltottKerdoivId){
        var kks = await _context.Kitoltottkerdoivs
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Programmings)
            .ThenInclude(x => x.KProgrammings)
            .ThenInclude(x => x.KProgrammingtestcases)
            .ThenInclude(x => x.Programmingtestcase)
            .SingleAsync(x => x.Id==kitoltottKerdoivId);
        var instance = kks.KProgrammings.First();
        return new RKitoltottP(instance, false);
    }

    public async Task<RKitoltottP> ViewSolvedAsUser(int kitoltottKerdoivId, int userId)
    {
        var kks = await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Programmings)
            .ThenInclude(x => x.KProgrammings)
            .ThenInclude(x => x.KProgrammingtestcases)
            .ThenInclude(x => x.Programmingtestcase)
            .SingleAsync(x => x.Id==kitoltottKerdoivId && x.Kitoltottallas.Allaskeresoid==userId);
        var instance = kks.KProgrammings.First();
        return new RKitoltottP(instance, true);
    }
}