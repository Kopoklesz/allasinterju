using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface IDesignService
{
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<int> Add(BDesignAdd da);
}
public class DesignService : IDesignService{
    private readonly AllasinterjuContext _context;
    public DesignService(AllasinterjuContext ctxt){
        _context=ctxt;
    }

    public async Task<int> Add(BDesignAdd da)
    {
        Kerdoiv k = new Kerdoiv{
            Nev=da.Name,
            Kor=da.Round,
            Allasid=da.JobId,
            Kitoltesperc=da.TimeLimit,
            Programming=false,
            Design=true,
            Algorithm=false,
            Testing=false,
            Devops=false
        };
        Design d = new Design{
            Kerdoiv=k,
            Title=da.Title,
            Category=da.Category,
            Description=da.Description,
            Styleguide=da.StyleGuide,
            Deliverables=da.Deliverables
        };
        foreach(var ev in da.EvalCriteria){
            d.Designevaluations.Add(new Designevaluation{
                Description=ev.Description,
                Weight=ev.Weight
            });
        }
        foreach(var re in da.ReferenceLinks){
            d.Designreferences.Add(new Designreference{
                Description=re.Description,
                Url=re.Url
            });
        }
        foreach(var req in da.DesignRequirements){
            d.Designreqs.Add(new Designreq{
                Description=req.Description,
                Category=req.Category
            });
        }
        await _context.AddRangeAsync(k, d);
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