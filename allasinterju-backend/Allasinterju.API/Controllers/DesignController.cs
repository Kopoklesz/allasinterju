using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DesignController : ControllerBase
{
    private readonly IDesignService _designService;
    public DesignController(IDesignService srvc){
        _designService=srvc;
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add(BDesignAdd da){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        bool userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Role).Value == "Ceg";
        if(await _designService.HasAuthority(da.JobId, userId, userRole)){
            return Ok(await _designService.Add(da));
        }
        return Unauthorized();
    }

}