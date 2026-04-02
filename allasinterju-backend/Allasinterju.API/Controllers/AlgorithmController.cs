using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AlgorithmController : ControllerBase
{
    private readonly IAlgorithmService _algorithmService;
    public AlgorithmController(IAlgorithmService srvc){
        _algorithmService=srvc;
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Add(BAlgorithmAdd da){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        bool userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type==ClaimTypes.Role).Value == "Ceg";
        if(await _algorithmService.HasAuthority(da.JobId, userId, userRole)){
            return Ok(await _algorithmService.Add(da));
        }
        return Unauthorized();
    }

}