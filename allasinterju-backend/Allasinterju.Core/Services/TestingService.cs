using System.Reflection.Metadata.Ecma335;
using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;

public interface ITestingService{
    Task<bool> HasAuthority(int allasId, int userId, bool isCompany);
    Task<int> Add(BTestingAdd ta);
}
public class TestingService : ITestingService{
    private readonly AllasinterjuContext _context;
    public TestingService(AllasinterjuContext ctxt){
        _context=ctxt;
    }

    public async Task<int> Add(BTestingAdd ta)
    {
        Kerdoiv k = new Kerdoiv{
            Nev=ta.Name,
            Kor=ta.Round,
            Allasid=ta.JobId,
            Kitoltesperc=ta.TimeLimit,
            Programming=false,
            Design=false,
            Algorithm=false,
            Testing=true,
            Devops=false
        };
        Testing t = new Testing{
            Kerdoiv=k,
            Title=ta.Title,
            Taskdesc=ta.Description,
            Testingtype=ta.Type,
            Appurl=ta.AppUrl,
            Os=ta.OS,
            Browser=ta.OS,
            Resolution=ta.Resolution,
            Additionalreq=ta.AdditionalRequirements,
            Stepstoreproduce=ta.StepsToReproduce,
            Expectedresult=ta.ExpectedResult,
            Actualresult=ta.ActualResult,
            Defaultpriority=ta.Priority,
            Defaultseverity=ta.Severity,
            Requireattachments=ta.RequireAttachments
        };
        foreach(var ca in ta.TestCases){
            var tc = new Testingcase{
                Title=ca.Title,
                Expectedresult=ca.ExpectedResult,
                Testdata=ca.TestData,
                Canbeautomated=ca.CanBeAutomated,
                Points=ca.Points
            };            
            foreach(var str in ca.Steps){
                var tcs = new Testingcasestep{
                    Teststep=str
                };
            }
            t.Testingcases.Add(tc);
        }
        foreach(var tool in ta.Tools){
            t.Testingtools.Add(new Testingtool{
                Name=tool.Name,
                Version=tool.Version,
                Purpose=tool.Purpose
            });
        }
        foreach(var eval in ta.EvaluationCriteria){
            t.Testingevaluations.Add(new Testingevaluation{
                Criterion=eval.Title,
                Weight=eval.Weight,
                Desc=eval.Description
            });
        }
        foreach(var sce in ta.Scenarios){
            t.Testingscenarios.Add(new Testingscenario{
                Title=sce.Title,
                Desc=sce.Description,
                Prereq=sce.Preconditions,
                Priority=sce.Priority
            });
        }
        await _context.AddRangeAsync(t, k);
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