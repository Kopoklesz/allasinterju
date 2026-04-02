using System.Net.Http.Headers;
using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public interface IAlgorithmService{
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<int> Add(BAlgorithmAdd aa);
}

public class AlgorithmService : IAlgorithmService{
    private readonly AllasinterjuContext _context;
    public AlgorithmService(AllasinterjuContext ctxt){
        _context=ctxt;
    }

    public async Task<int> Add(BAlgorithmAdd aa)
    {
        Kerdoiv k = new Kerdoiv{
            Nev=aa.Name,
            Kor=aa.Round,
            Allasid=aa.JobId,
            Kitoltesperc=aa.TimeLimit,
            Programming=false,
            Design=false,
            Algorithm=true,
            Testing=false,
            Devops=false
        };
        /*await _context.AddAsync(k);
        await _context.SaveChangesAsync();*/
        Algorithm a = new Algorithm{
            Title=aa.Title,
            Category=aa.Category,
            Difficulty=aa.Difficulty,
            Problemdesc=aa.Description,
            Inputformat=aa.InputFormat,
            Outputformat=aa.OutputFormat,
            Timecomplexity=aa.TimeComplexity,
            Spacecomplexity=aa.SpaceComplexity,
            Samplesolution=aa.SampleSolution,
            Kerdoiv=k
        };
        foreach(var co in aa.Constraints){
            a.Algorithmconstraints.Add(new Algorithmconstraint{
                Constraint=co
            });
        }
        foreach(var exa in aa.Examples){
            a.Algorithmexamples.Add(new Algorithmexample{
                Input=exa.Input,
                Output=exa.Output,
                Explanation=exa.Explanation
            });
        }
        foreach(var hi in aa.Hints){
            a.Algorithmhints.Add(new Algorithmhint{
                Hint=hi
            });
        }
        await _context.AddRangeAsync(k, a);
        await _context.SaveChangesAsync();
        return k.Id;
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
}