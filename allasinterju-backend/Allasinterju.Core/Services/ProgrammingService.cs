using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface IProgrammingService
{
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<int> Add(BProgrammingAdd pa);
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
}