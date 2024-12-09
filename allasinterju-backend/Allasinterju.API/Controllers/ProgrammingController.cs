using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgrammingController : ControllerBase
{
    private readonly IProgrammingService _programmingService;
    public ProgrammingController(IProgrammingService srvc){
        _programmingService=srvc;
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add(BProgrammingAdd pa){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        bool userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Role).Value == "Ceg";
        if(await _programmingService.HasAuthority(pa.JobId, userId, userRole)){
            return Ok(await _programmingService.Add(pa));
        }
        return Unauthorized();
    }

    [HttpPut("Modify")]
    public async Task<IActionResult> Modify(BProgrammingModify pm){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        bool userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Role).Value == "Ceg";
        if(await _programmingService.HasAuthority(pm.JobId, userId, userRole)){
            await _programmingService.Modify(pm);
            return Ok();
        }
        return Unauthorized();
    }

    [HttpPost("Solve")]    
    public async Task<IActionResult> Solve(int kerdoivId){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        if(await _programmingService.IsSolvable(kerdoivId, userId)){
            return Ok(await _programmingService.Solve(kerdoivId, userId));
        }
        return Unauthorized("Not solvable.");
    }

    [HttpPost("SaveProgress")]
    public async Task<IActionResult> SaveProgress(BSaveProgressP sp){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        if(await _programmingService.IsSolvable(sp.KerdoivId, userId)){
            await _programmingService.SaveProgress(sp, userId, false);
            return Ok();
        }
        return Unauthorized("Cannot save progress.");
    }

    [HttpPost("Finish")]
    public async Task<IActionResult> Finish(BSaveProgressP sp){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        if(await _programmingService.IsSolvable(sp.KerdoivId, userId)){
            await _programmingService.SaveProgress(sp, userId, true);
            return Ok();
        }
        return Unauthorized("Cannot save progress.");
    }

    [HttpPost("FinishUponTimeout")]
    public async Task<IActionResult> FinishUponTimeout(BSaveProgressP sp){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        if(await _programmingService.IsSolvable(sp.KerdoivId, userId, 2)){
            await _programmingService.SaveProgress(sp, userId, true);
            return Ok();
        }
        return Unauthorized("Cannot save progress.");
    }
    [HttpGet("ViewSolved")]
    [Authorize(Roles="Ceg,Dolgozo")]
    public async Task<IActionResult> ViewSolved(BUserKerdoivIds uki){
        try{
            int kitoltottKerdoivId = await _programmingService.GetKKID(uki.MunkakeresoId, uki.KerdoivId);
            return Ok(await _programmingService.ViewSolved(kitoltottKerdoivId));
        }
        catch{
            return Unauthorized("Not viewable.");
        }
    }

    [HttpPut("ViewAllSolvedPerUser")]
    [Authorize(Roles="Ceg,Dolgozo")]
    public async Task<IActionResult> ViewAllSolvedPerUser(BUserAllasIds uai){
        //try{
            //int kitoltottKerdoivId = await _programmingService.GetKKID(uki.MunkakeresoId, uki.KerdoivId);
            return Ok(await _programmingService.ViewAllSolvedPerUser(uai));
        //}
        //catch{
        //    return Unauthorized("Not viewable.");
        //}
    }

    [HttpGet("ViewSolvedAsUser/{kitoltottKerdoivId:int}")]
    [Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> ViewSolvedAsUser(int kitoltottKerdoivId){
        try{
            int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
            return Ok(await _programmingService.ViewSolvedAsUser(kitoltottKerdoivId, userId));
        }
        catch{
            return Unauthorized("Not viewable.");
        }
    }
}