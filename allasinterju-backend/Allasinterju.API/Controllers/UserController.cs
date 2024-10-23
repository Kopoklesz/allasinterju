using Microsoft.AspNetCore.Mvc;

namespace Allasinterju.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly IUserService _userService;

    public UserController(IUserService srvc)
    {
        _userService = srvc;
    }

    [HttpGet("ById/{id:int}")]
    public async Task<IActionResult> ById(int id)
    {
        if(_userService.Exists(id))
            return Ok(await _userService.ById(id));
        return NotFound("User not found.");
    }
    [HttpGet("GetAppliedJobs/{id:int}")]
    public async Task<IActionResult> GetAppliedJobs(int id){
        if(_userService.Exists(id))
            return Ok(await _userService.GetAppliedJobs(id));
        return NotFound("User not found.");
    }
}
