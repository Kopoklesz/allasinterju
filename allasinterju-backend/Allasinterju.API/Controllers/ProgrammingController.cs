using System.Security.Claims;
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

}