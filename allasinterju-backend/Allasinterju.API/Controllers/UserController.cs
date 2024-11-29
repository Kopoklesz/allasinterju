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
    [HttpPost("Login")]
    public async Task<IActionResult> Login(DtoLogin login){
        if(await _userService.Login(login.Username, login.Password)){
            string token = await _userService.GetToken(login.Username);
            var cookieOptions = new CookieOptions{
                HttpOnly = false,
                Secure = false,
                MaxAge = TimeSpan.FromDays(30),
                SameSite = SameSiteMode.Strict,
                Path = "/"
            };
           
            Response.Cookies.Append("JWT_TOKEN", token, cookieOptions);            
            Console.WriteLine(HttpContext.User.IsInRole("Admin"));
            return Ok();
        }
        return Unauthorized();

    }
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout(){
        Response.Cookies.Delete("JWT_TOKEN");
        HttpContext.User=null;
        return Ok();
    }

    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser(DtoUserRegister user){
        if(await _userService.IsUnique(user.EmailAddress)){
            await _userService.RegisterUser(user);
            string token = await _userService.GetToken(user.EmailAddress);
            var cookieOptions = new CookieOptions{
                HttpOnly = true,
                Secure = false,
                MaxAge = TimeSpan.FromDays(30),
                SameSite = SameSiteMode.Strict,
                Path = "/"
            };
            Response.Cookies.Append("JWT_TOKEN", token, cookieOptions);            
            Console.WriteLine(HttpContext.User.IsInRole("Admin"));
            return Ok();
        }
        return NotFound();
    }

    [HttpPost("RegisterCompany")]
    public async Task<IActionResult> RegisterCompany(DtoCompanyRegister comp){
        if(await _userService.IsUnique(comp.Email)){
            await _userService.RegisterCompany(comp);
            string token = await _userService.GetToken(comp.Email);
            var cookieOptions = new CookieOptions{
                HttpOnly = true,
                Secure = false,
                MaxAge = TimeSpan.FromDays(30),
                SameSite = SameSiteMode.Strict,
                Path = "/"
            };
            Response.Cookies.Append("JWT_TOKEN", token, cookieOptions);            
            Console.WriteLine(HttpContext.User.IsInRole("Admin"));
            return Ok();
        }
        return NotFound();
    }
}
//LEETCODE??
