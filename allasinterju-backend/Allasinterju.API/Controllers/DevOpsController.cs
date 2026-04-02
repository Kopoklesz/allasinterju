using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DevOpsController : ControllerBase
{
    private readonly IDevOpsService _devOpsService;
    public DevOpsController(IDevOpsService srvc){
        _devOpsService=srvc;
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add(BDevOpsAdd da){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        bool userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Role).Value == "Ceg";
        if(await _devOpsService.HasAuthority(da.JobId, userId, userRole)){
            return Ok(await _devOpsService.Add(da));
        }
        return Unauthorized();
    }

}