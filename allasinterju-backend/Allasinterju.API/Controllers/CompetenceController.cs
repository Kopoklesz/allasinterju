using Microsoft.AspNetCore.Mvc;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CompetenceController : ControllerBase
{
    
    private readonly ICompetenceService _competenceService;

    public CompetenceController(ICompetenceService srvc)
    {
        _competenceService = srvc;
    }
    [HttpGet("All")]
    public async Task<IActionResult> GetAllCompetences(){
        return Ok(_competenceService.GetAll());
    }

    [HttpPost("AddToUser")]
    public async Task<IActionResult> AddToUser(BCompetence comp){
        int id=int.Parse(HttpContext.User.Claims.First(x => x.Type=="id").Value);
        await _competenceService.AddToUser(comp.Type, id, comp.Level);
        return Ok();
    }

    [HttpPost("AddToJob")]
    public async Task<IActionResult> AddToJob(DtoCompetenceJob cj){
        await _competenceService.AddToJob(cj.Type, cj.JobId, cj.Type);
        return Ok();
    }

    [HttpGet("GetForUser")]
    public async Task<IActionResult> GetForUser(){
        int id=int.Parse(HttpContext.User.Claims.First(x => x.Type=="id").Value);
        return Ok(_competenceService.GetForUser(id));
    }

    [HttpGet("GetForJob/{jobId:int}")]
    public async Task<IActionResult> GetForJob(int jobId){        
        return Ok(_competenceService.GetForJob(jobId));
    }

    [HttpDelete("DeleteForUser/{id:int}")]
    public async Task<IActionResult> DeleteForUser(int id){
        int userId=int.Parse(HttpContext.User.Claims.First(x => x.Type=="id").Value);
        await _competenceService.DeleteForUser(userId, id);
        return Ok();
    }

    [HttpDelete("DeleteForJob")]
    public async Task<IActionResult> DeleteForUser(DtoCompetenceJobDelete cjd){
        await _competenceService.DeleteForJob(cjd.JobId, cjd.Id);
        return Ok();
    }
}