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
}
