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
    
}
