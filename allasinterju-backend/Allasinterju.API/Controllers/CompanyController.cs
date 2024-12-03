using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService srvc)
    {
        _companyService = srvc;
    }

    [HttpGet("ById/{id:int}")]
    public async Task<IActionResult> ById(int id)
    {
        return Ok(await _companyService.GetCompanyById(id));
    }
    [HttpGet("GetAdvertisedJobs/{id:int}")]
    public async Task<IActionResult> GetAdvertisedJobs(int id)
    {
        return Ok(await _companyService.GetAdvertisedJobs(id));
    }
    [HttpPost("CreateInvite")]
    public async Task<IActionResult> CreateInvite(DtoInvitation invite){
        int companyId = int.Parse(HttpContext.User.Claims.First(x => x.Type=="id").Value);
        await _companyService.CreateInvite(invite, companyId);
        return Ok();
    }
    [HttpGet("GetAllInvites")]
    public async Task<IActionResult> GetAllInvites(){
        int companyId = int.Parse(HttpContext.User.Claims.First(x => x.Type=="id").Value);
        return Ok(_companyService.GetAllInvites(companyId));
    }
    [HttpDelete("DeleteInvite/{id:int}")]
    public async Task<IActionResult> DeleteInvite(int id){
        int companyId = int.Parse(HttpContext.User.Claims.First(x => x.Type=="id").Value);
        try{
            _companyService.DeleteInvite(id, companyId);
            return Ok();
        }
        catch{
            return Unauthorized();
        }
    }

    [HttpGet("GetWorkers")]
    [Authorize(Roles="Ceg")]
    public async Task<IActionResult> GetWorkers(){
        int companyId = int.Parse(HttpContext.User.Claims.First(x => x.Type=="id").Value);
        return Ok(await _companyService.GetWorkers(companyId));
    }

    [HttpDelete("RemoveWorker/{workerId:int}")]
    [Authorize(Roles="Ceg")]
    public async Task<IActionResult> RemoveWorker(int workerId){
        int companyId = int.Parse(HttpContext.User.Claims.First(x => x.Type=="id").Value);
        try{
            await _companyService.RemoveWorker(workerId, companyId);
            return Ok();
        }
        catch{
            return Unauthorized();
        }
    }

    [HttpPost("Nominate")]
    [Authorize(Roles="Ceg,Dolgozo")]
    public async Task<IActionResult> InviteWorker(BInviteToApplication ita){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        bool userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Role).Value == "Ceg";
        if(await _companyService.HasAuthority(ita.AllasId, userId, userRole)){
            await _companyService.Nominate(ita);
        }
        return Unauthorized();
    }

    [HttpGet("ListNominationsCompany")]
    [Authorize(Roles="Ceg,Dolgozo")]
    public async Task<IActionResult> ListNominationsCompany(){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        bool userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Role).Value == "Ceg";
        if(!userRole){
            userId = await _companyService.GetCompanyIdByWorkerId(userId);
        }
        return Ok(await _companyService.ListNominations(userId));
    }

    [HttpGet("ListAllJobSeekers")]
    [Authorize(Roles="Ceg,Dolgozo")]
    public async Task<IActionResult> ListAllJobSeekers(){
        return Ok(await _companyService.ListAllJobSeekers());
    }

    [HttpGet("JobSeekerReport/{jobSeekerId:int}")]
    [Authorize(Roles="Ceg,Dolgozo")]
    public async Task<IActionResult> JobSeekerReport(int jobSeekerId){
        return Ok(await _companyService.JobSeekerReport(jobSeekerId));
    }
}
