using System.Reflection.Metadata.Ecma335;
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
    //Task SanitizeAlt();
    Task SaveProgress(BSaveProgressP sp, int userId, bool finish);
    Task<RKitoltottP> ViewSolved(int kitoltottKerdoivId);
    Task<RKitoltottP> ViewSolvedAsUser(int kitoltottKerdoivId, int userId);
    Task<int> GetKKID(int userId, int kerdoivId);
    Task<List<RKitoltottP>> ViewAllSolvedPerUser(BUserAllasIds uai);
}
public class ProgrammingService : IProgrammingService{
    private readonly AllasinterjuContext _context;
    private readonly IPistonClient _pistonClient;
    public ProgrammingService(AllasinterjuContext ctxt, IPistonClient clnt){
        _context=ctxt;
        _pistonClient=clnt;
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
    private async Task RunCode(KProgramming kp){
        var testcases = kp.Programming.Programmingtestcases;
        var code = kp.Programkod;
        var lang = kp.Programming.Language;
        foreach(var tc in testcases){
            var resp = await _pistonClient.ExecuteCodeAsync(code, tc.Input ?? "", lang);
            KProgrammingtestcase kptc = new KProgrammingtestcase{
                Programmingtestcaseid=tc.Id
            };
            if(resp.Run!=null){
                kptc.Lefutott=true;
                kptc.Stdout=resp.Run.Stdout;
                kptc.Stderr=resp.Run.Stderr;
                kptc.Memoria=resp.Run.Memory;
                kptc.Nemfutle=false;
                if(kptc.Stdout==tc.Output){
                    kptc.Helyes=true;
                }
                else{
                    kptc.Helyes=false;
                }
            }
            else{
                kptc.Lefutott=true;
                kptc.Nemfutle=true;
            }
            kp.KProgrammingtestcases.Add(kptc);
        }
        await _context.SaveChangesAsync();
    }
    public async Task Sanitize(){
        /*var kk = _context.Kitoltottkerdoivs
        .AsNoTracking()
        .Include(x => x.KProgrammings)
        .Include(x => x.Kerdoiv)
        .ThenInclude(x => x.Programmings)
        .ThenInclude(x => x.Programmingtestcases)
        .Where(x => x.Befejezve == false && ((DateTime)x.Kitolteskezdet).AddMinutes((double)x.Kerdoiv.Kitoltesperc).AddMinutes(2) < DateTime.Now)
        .AsQueryable();
        foreach(var elem in kk){
            elem.Befejezve=true;
            if(elem.Kerdoiv.Programming==true){
                await Task.Run(() => RunCode(elem.KProgrammings.First()));
            }
        }
        await _context.SaveChangesAsync();*/
    }
    /*public async Task SanitizeAlt()
    {
        var query = @"
        SELECT 
            [k].[id] AS [KitoltottKerdoivId], 
            [k].[befejezve], 
            [k].[kitolteskezdet], 
            [k].[kerdoivid] AS [KerdoivId], 
            [k0].[kitoltesperc] AS [KitoltesPerc], 
            [k0].[programming] AS [Programming], 
            [k1].[programkod] AS [ProgramKod]
        FROM [kitoltottkerdoiv] AS [k]
        INNER JOIN [kerdoiv] AS [k0] ON [k].[kerdoivid] = [k0].[id]
        LEFT JOIN [k_programming] AS [k1] ON [k].[id] = [k1].[kitoltottkerdoivid]
        WHERE [k].[befejezve] = CAST(0 AS bit) 
        AND DATEADD(minute, 2, DATEADD(minute, [k0].[kitoltesperc], [k].[kitolteskezdet])) < GETDATE()";

        var results = await _context.SanitizeAlts.FromSqlRaw(query).ToListAsync();

        foreach (var elem in results)
        {
            if (!elem.Befejezve)
            {
                elem.Befejezve = true;

                if (elem.Programming == true && !string.IsNullOrEmpty(elem.ProgramKod))
                {
                    await Task.Run(() => RunCode(elem.ProgramKod));
                }
            }
        }

        // Save changes back to the database
        foreach (var result in results)
        {
            var entity = await _context.Kitoltottkerdoivs.FindAsync(result.KitoltottKerdoivId);
            if (entity != null)
            {
                entity.Befejezve = result.Befejezve;
            }
        }

        await _context.SaveChangesAsync();
    }*/
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
        try{
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
        catch{
            return true;
        }
    }

    public async Task<RSolveP> Solve(int kerdoivId, int userId)
    {
        var prog = await _context.Programmings
            .Include(x => x.Kerdoiv)
            .Include(x => x.KProgrammings)
            .ThenInclude(x => x.Kitoltottkerdoiv)
            .SingleAsync(x => x.Kerdoivid==kerdoivId);
        if(prog.KProgrammings.Count()==0){
            var allasInstance = await _context.Allas.Include(x => x.Kerdoivs).SingleAsync(x => x.Kerdoivs.Any(y => y.Id==kerdoivId));
            var ka = await _context.Kitoltottallas.SingleAsync(x => x.Allaskeresoid==userId && x.Allasid==allasInstance.Id);
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
        Console.WriteLine(kerdoivId);
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
        var kk = await _context.Kitoltottkerdoivs
            .Include(x => x.KProgrammings)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Programmings)
            .ThenInclude(x => x.Programmingtestcases)
            .SingleAsync(x => x.Kerdoivid==sp.KerdoivId && x.Kitoltottallasid==ka.Id);
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
            await Task.Run(() => RunCode(kk.KProgrammings.First()));
        }
        await _context.SaveChangesAsync();
    }

    public async Task<RKitoltottP> ViewSolved(int kitoltottKerdoivId){
        var kks = await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Algorithms)
            .ThenInclude(x => x.KTobbis)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Designs)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.DevopsNavigation)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Testings)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Programmings)
            .ThenInclude(x => x.KProgrammings)
            .ThenInclude(x => x.KProgrammingtestcases)
            .ThenInclude(x => x.Programmingtestcase)
            .SingleAsync(x => x.Id==kitoltottKerdoivId);                
        if(kks.Kerdoiv.Programming==true){
            var instance = kks.KProgrammings.First();
            return new RKitoltottP(instance, true);
        }
        var instance2 = kks.KTobbis.First();
        return new RKitoltottP(instance2);
    }

    public async Task<RKitoltottP> ViewSolvedAsUser(int kitoltottKerdoivId, int userId)
    {
        var kks = await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Algorithms)
            .ThenInclude(x => x.KTobbis)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Designs)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.DevopsNavigation)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Testings)
            .Include(x => x.Kerdoiv)
            .ThenInclude(x => x.Programmings)
            .ThenInclude(x => x.KProgrammings)
            .ThenInclude(x => x.KProgrammingtestcases)
            .ThenInclude(x => x.Programmingtestcase)
            .SingleAsync(x => x.Id==kitoltottKerdoivId && x.Kitoltottallas.Allaskeresoid==userId);                
        if(kks.Kerdoiv.Programming==true){
            var instance = kks.KProgrammings.First();
            return new RKitoltottP(instance, true);
        }
        var instance2 = kks.KTobbis.First();
        return new RKitoltottP(instance2);
    }

    public async Task<int> GetKKID(int userId, int kerdoivId)
    {
        var instance =await _context.Kitoltottkerdoivs
            .Include(x => x.Kitoltottallas)
            .SingleAsync(x => x.Kitoltottallas.Allaskeresoid==userId && x.Kerdoivid==kerdoivId);
        return instance.Id;
    }

    public async Task<List<RKitoltottP>> ViewAllSolvedPerUser(BUserAllasIds uai)
    {
        var ka = await _context.Kitoltottallas
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .ThenInclude(x => x.Algorithms)
            .ThenInclude(x => x.KTobbis)
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .ThenInclude(x => x.Designs)
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .ThenInclude(x => x.DevopsNavigation)
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .ThenInclude(x => x.Testings)
            .Include(x => x.Kitoltottkerdoivs)
            .ThenInclude(x => x.Kerdoiv)
            .ThenInclude(x => x.Programmings)
            .ThenInclude(x => x.KProgrammings)
            .ThenInclude(x => x.KProgrammingtestcases)
            .ThenInclude(x => x.Programmingtestcase)
            .SingleAsync(x => x.Allasid==uai.AllasId && x.Allaskeresoid==uai.MunkakeresoId); 
        List<RKitoltottP> resp = new List<RKitoltottP>();
        foreach(var kk in ka.Kitoltottkerdoivs){
            if(kk.Kerdoiv.Programming==true){
                var instance = kk.KProgrammings.First();
                resp.Add(new RKitoltottP(instance, false));
            }
            else{
                var instance2 = kk.KTobbis.First();
                resp.Add(new RKitoltottP(instance2));
            }            
        }
        return resp;
    }
}