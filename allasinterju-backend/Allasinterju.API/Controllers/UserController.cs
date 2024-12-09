using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
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
        return NotFound("A user already exists with the given email address.");
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
        return NotFound("A user already exists with the given email address.");
    }

    [HttpPost("SetLeetcodeUsername/{username}")]
    [Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> SetLeetcodeUsername(string username){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        await _userService.SetLeetcodeUsername(username, userId);
        return Ok();
    }

    [HttpGet("GetLeetcodeStats/{userId:int}")]
    public async Task<IActionResult> GetLeetcodeStats(int userId){
        try{
            return Ok(await _userService.GetLeetcodeStats(userId));
        }
        catch{
            return NotFound("Leetcode username not found.");
        }
    }

    [HttpGet("ListNominations")]
    public async Task<IActionResult> ListNominations(){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        return Ok(await _userService.ListNominations(userId));
    }

    [HttpGet("PendingNominationCount")]
    public async Task<IActionResult> PendingNominationCount(){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        return Ok(await _userService.PendingNominationCount(userId));
    }

    [HttpPut("Modify")]
    [Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> Modify(BUserModify um){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        await _userService.Modify(userId, um);
        return Ok();
    }

    [HttpPost("UploadDocument")]
    [Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> UploadDocument(BDokumentumFeltoltes df){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        await _userService.DocumentUpload(df, userId);
        return Ok();
    }

    [HttpGet("DownloadDocument/{documentId:int}")]
    public async Task<IActionResult> DownloadDocument(int documentId){        
        return File(await _userService.DocumentData(documentId),
             "application/pdf",
              await _userService.DocumentName(documentId));
    }

    [HttpDelete("DeleteDocument/{documentId:int}")]
    [Authorize(Roles="Munkakereso")]
    public async Task<IActionResult> DeleteDocument(int documentId){
        int userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type=="id").Value);
        await _userService.DeleteDocument(documentId);
        return Ok();
    }

}
