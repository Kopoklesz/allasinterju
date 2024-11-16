using System.Security.Claims;
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
}
