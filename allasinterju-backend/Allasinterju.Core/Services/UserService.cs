using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Allasinterju.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;

public interface IUserService{
    Task<DtoUser> ById(int id);
    bool Exists(int id);
    Task<List<DtoJobShort>> GetAppliedJobs(int id);
    Task<bool> Login(string username, string password);
    Task<string> GetToken(string username);
}
public class UserService : IUserService
{
    private readonly AllasinterjuContext _context;

    private readonly HMACSHA512 hmac;
    private const string SECRET = "oooooooooooooooooooooooooooooooooooooooooooooooo";

    public UserService(AllasinterjuContext ctxt){
        _context = ctxt;
        byte[] key = Encoding.ASCII.GetBytes(SECRET);
        hmac = new HMACSHA512(key);

    }
    public async Task<DtoUser> ById(int id)
    {
        return new DtoUser(
            await _context.Felhasznalos
                .Include(x => x.Kitoltottallas)
                .ThenInclude(x => x.Allas)
                .SingleAsync(x => x.Id==id)
        );
    }

    public bool Exists(int id)
    {
        return _context.Felhasznalos.Where(x => x.Id==id).Count()==1;
    }

    public async Task<List<DtoJobShort>> GetAppliedJobs(int id)
    {
        return await _context.Felhasznalos
                .Include(x => x.Kitoltottallas)
                .ThenInclude(x => x.Allas)
                .Where(x => x.Id==id)
                .SelectMany(x => x.Kitoltottallas)
                .Select(x => new DtoJobShort(x.Allas)).ToListAsync();
    }
    private string ComputeHash(string password){
        byte[] data = Encoding.ASCII.GetBytes(password);
        //string ass = BitConverter.ToString(hmac.ComputeHash(data));
        return Convert.ToBase64String(hmac.ComputeHash(data));
    }

    public async Task<bool> Login(string username, string password)
    {
        var nemCegUser = await _context.Felhasznalos.SingleOrDefaultAsync(x => x.Email==username && x.Jelszo==ComputeHash(password));
        var cegUser = await _context.Cegs.SingleOrDefaultAsync(x => x.Email==username && x.Jelszo==ComputeHash(password));
        if(nemCegUser==null && cegUser==null){
            return false;
        }
        return true;
    }
    public async Task<string> GetToken(string user){
        var currUser = await _context.Felhasznalos.SingleOrDefaultAsync(x => x.Email == user);
        var currCeg = await _context.Cegs.SingleOrDefaultAsync(x => x.Email == user);
        int id = currUser!=null ? currUser.Id : currCeg.Id;
        string role = "";
        if(currCeg!=null){
            role = "Ceg";
        }
        else if(!currUser.Dolgozo && !currUser.Allaskereso){
            role = "Admin";
        }
        else if(currUser.Dolgozo){
            role = "Dolgozo";
        }
        else if(currUser.Allaskereso){
            role = "Munkakereso";
        }
        return GenerateJWTToken(user,id.ToString(),role);
    }
    private string GenerateJWTToken(string username, string id, string role){
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(SECRET);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("id", id),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddDays(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            Issuer="hu.jobhub"            
        };
        //Console.WriteLine(username + " " + id + " " + role);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }


}