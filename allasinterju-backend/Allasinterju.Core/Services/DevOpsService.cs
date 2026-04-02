using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface IDevOpsService{
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<int> Add(BDevOpsAdd da);
}
public class DevOpsService : IDevOpsService{
    private readonly AllasinterjuContext _context;
    public DevOpsService(AllasinterjuContext ctxt){
        _context=ctxt;
    }

    public async Task<int> Add(BDevOpsAdd da)
    {
        Kerdoiv k = new Kerdoiv{
            Nev=da.Name,
            Kor=da.Round,
            Allasid=da.JobId,
            Kitoltesperc=da.TimeLimit,
            Programming=false,
            Design=false,
            Algorithm=false,
            Testing=false,
            Devops=true
        };
        Devop d = new Devop{
            Kerdoiv=k,
            Tasktitle=da.Title,
            Category=da.Category,
            Difficulty=da.Difficulty,
            Taskdescription=da.Description,
            Platform=da.Platform,
            Systemrequirements=da.SystemRequirements,
            Accessrequirements=da.AccessRequirements,
            Architecturedesc=da.ArchitectrueDescription,
            Infraconstraints=da.InfrastructureConstraints,
            Docrequired=da.DocumentationRequired,
            Docformat=da.DocumentationFormat,
            Resourcelimits=da.ResourceLimits
        };
        foreach(var co in da.Components){
            d.Devopscomponents.Add(new Devopscomponent{
                Name=co.Name,
                Type=co.Type,
                Configuration=co.Configuration
            });
        }
        foreach(var del in da.Deliverables){
            d.Devopsdeliverables.Add(new Devopsdeliverable{
                Title=del.Title,
                Desc=del.Description,
                Acceptance=del.Acceptance,
                Format=del.Format
            });
        }
        foreach(var eval in da.EvaluationCriteria){
            d.Devopsevaluations.Add(new Devopsevaluation{
                Criterion=eval.Title,
                Weight=eval.Weight,
                Desc=eval.Description
            });
        }
        foreach(var doc in da.DocTemplates){
            d.Devopsdocumentations.Add(new Devopsdocumentation{
                Title=doc.Title,
                Templatecontent=doc.Content,
                Requiredtemplate=doc.Required
            });
        }
        foreach(var pre in da.Prerequisites){
            d.Devopsprereqs.Add(new Devopsprereq{
                Tool=pre.Tool,
                Version=pre.Version,
                Purpose=pre.Purpose
            });
        }
        foreach(var ta in da.Tasks){
            Devopstask dt = new Devopstask{
                Title=ta.Title,
                Desc=ta.Description,
                Validation=ta.Validation,
                Points=ta.Points
            };
            foreach(var impl in ta.Steps){
                dt.Devopstaskimplementations.Add(new Devopstaskimplementation{
                    Implementation=impl
                });
            }
            d.Devopstasks.Add(dt);
        }
        await _context.AddRangeAsync(d, k);
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