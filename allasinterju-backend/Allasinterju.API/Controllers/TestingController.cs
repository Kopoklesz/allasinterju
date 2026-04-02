using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestingController : ControllerBase
{
    private readonly ITestingService _testingService;
    public TestingController(ITestingService srvc){
        _testingService=srvc;
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add(BTestingAdd da){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        bool userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Role).Value == "Ceg";
        if(await _testingService.HasAuthority(da.JobId, userId, userRole)){
            return Ok(await _testingService.Add(da));
        }
        return Unauthorized();
    }

}