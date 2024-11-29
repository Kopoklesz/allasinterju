using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Hosting;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    
    private readonly IJobService _jobService;

    public JobController(IJobService srvc)
    {
        _jobService = srvc;
    }

    [HttpGet("GetAllJobs")]
    public async Task<IActionResult> GetAllJobs()
    {
        return Ok(await _jobService.GetAllJobs());
    }
    [HttpGet("ById/{id:int}")]
    public async Task<IActionResult> ById(int id){
        return Ok(await _jobService.ById(id));
    }
    [HttpPost("AddJob")]
    [Authorize(Roles="Ceg")]
    public async Task<IActionResult> AddJob(DtoJobAdd job){
        int id = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        if(_jobService.CompanyExists(id)){
            await _jobService.AddJob(job, id);
            return Ok();
        }
        return Unauthorized();
    }
    [HttpPost("Apply/{jobId:int}")]
    public async Task<IActionResult> Apply(int jobId){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        _jobService.ApplyForJob(jobId, userId);
        return Ok();
    }
    [HttpPost("SaveProgress")]
    [Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> SaveProgress(DtoSaveProgress sp){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        if(await _jobService.IsWithinTimeFrame(sp.KerdoivId, userId)){
            await _jobService.SaveProgress(sp, userId, false);
            return Ok();
        }
        return Unauthorized("Out of time.");
    }
    [HttpPost("Finish")]
    [Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> Finish(DtoSaveProgress sp){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        if(await _jobService.IsWithinTimeFrame(sp.KerdoivId, userId)){
            await _jobService.SaveProgress(sp, userId, true);
            return Ok();
        }
        return Unauthorized("Out of time.");
    }
    [HttpGet("GetNextFreshRoundForUser/{allasId:int}")]
    [Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> GetRound(int allasId){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        return Ok(await _jobService.GetNextFreshRoundForUser(allasId, userId));
    }
    [HttpGet("GetRoundForCompany/{kerdoivId:int}")]
    //[Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> GetRoundForCompany(int kerdoivId){
        return Ok(await _jobService.GetRoundForCompany(kerdoivId));
    }
    [HttpGet("GetRoundSummary/{kerdoivId:int}")]
    public async Task<IActionResult> GetRoundSummary(int kerdoivId){
        return Ok(await _jobService.GetRoundSummary(kerdoivId));
    }
    [HttpPost("AddRound")]
    public async Task<IActionResult> AddRound(DtoKerdoivLetrehozas klh){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        //await _jobService.HasAuthority(klh.AllasId, userId)
        if(true){
            await _jobService.AddRound(klh);
            return Ok();
        }
        return Unauthorized();
    }
    [HttpGet("GetRoundsShort/{jobId:int}")]
    public async Task<IActionResult> GetRoundsShort(int jobId){
        return Ok(_jobService.GetRoundsShort(jobId));
    }
    
}
