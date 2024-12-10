using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks.Dataflow;
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
    Task<bool> IsUnique(string email);
    Task RegisterCompany(DtoCompanyRegister comp);
    Task RegisterUser(DtoUserRegister user);
    Task SetLeetcodeUsername(string username, int userId);
    Task<LeetcodeResponse> GetLeetcodeStats(int userId);
    Task<List<RNomination>> ListNominations(int userId);
    Task<int> PendingNominationCount(int userId);
    Task Modify(int userId, BUserModify um);
    Task DocumentUpload(BDokumentumFeltoltes df, int userId);
    Task<byte[]> DocumentData(int documentId);
    Task<string> DocumentName(int documentId);
    Task DeleteDocument(int documentId);
}
public class UserService : IUserService
{
    private readonly AllasinterjuContext _context;

    private readonly HMACSHA512 hmac;
    private readonly ILeetcodeClient _leetcode;
    private const string SECRET = "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";

    public UserService(AllasinterjuContext ctxt, ILeetcodeClient ltcd){
        _context = ctxt;
        byte[] key = Encoding.ASCII.GetBytes(SECRET);
        hmac = new HMACSHA512(key);
        _leetcode=ltcd;
    }
    public async Task<DtoUser> ById(int id)
    {
        return new DtoUser(
            await _context.Felhasznalos
                .Include(x => x.Felhasznalokompetencia)
                .ThenInclude(x => x.Kompetencia)
                .Include(x => x.Vegzettsegs)
                .Include(x => x.Kitoltottallas)
                .ThenInclude(x => x.Allas)
                .ThenInclude(x => x.Ceg)
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
                .ThenInclude(x => x.Ceg)
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

    public async Task<bool> IsUnique(string email)
    {
        var users = await _context.Felhasznalos.AnyAsync(x => x.Email==email);
        var companies = await _context.Cegs.AnyAsync(x => x.Email==email);
        return !users && !companies;
    }

    public async Task RegisterCompany(DtoCompanyRegister comp)
    {
        Ceg c = new Ceg{
            Email=comp.Email,
            Jelszo=ComputeHash(comp.Password),
            Cegnev=comp.CompanyName,
            Cegtipus=comp.CompanyType,
            Leiras=comp.Description,
            Levelezesicim=comp.MailingAddress,
            Kapcsolattarto=comp.HREmployee,
            Mobiltelefon=comp.MobilePhoneNumber,
            Telefon=comp.CablePhoneNumber,
            Telephely=comp.Place.ZipCode+" "+comp.Place.City+" "+comp.Place.StreetNumber
            
        };
        _context.Cegs.Add(c);
        await _context.SaveChangesAsync();
    }

    public async Task RegisterUser(DtoUserRegister user)
    {
        bool dolgozo = false;
        bool munkakereso = false;
        Meghivokod? kod = await _context.Meghivokods.SingleOrDefaultAsync(x => x.Kod==user.InvitationCode && x.Ervenyesseg<DateTime.Now);
        if(kod==null){
            munkakereso=true;
        }
        else{
            dolgozo=true;            
        }
        Felhasznalo f = new Felhasznalo{
            Vezeteknev = user.LastName,
            Keresztnev = user.FirstName,
            Email = user.EmailAddress,
            Jelszo = ComputeHash(user.Password),
            Adoszam = user.TaxNumber,
            Anyjaneve = user.MothersName,
            Szuldat = user.BirthDate,
            Szulhely = user.BirthPlace,
            Dolgozo = dolgozo,
            Allaskereso = munkakereso,
            Cegid = dolgozo ? kod.Cegid : null,
        };
        if(kod!=null){
            _context.Remove(kod);
        }
        await _context.AddAsync(f);
        await _context.SaveChangesAsync();
    }

    public async Task SetLeetcodeUsername(string username, int userId)
    {
        var felh = await _context.Felhasznalos.SingleAsync(x => x.Id==userId);
        felh.Leetcode=username;
        await _context.SaveChangesAsync();
        return; 
    }

    public async Task<LeetcodeResponse> GetLeetcodeStats(int userId)
    {
        var felh = await _context.Felhasznalos.SingleAsync(x => x.Id==userId);
        Console.WriteLine(felh.Leetcode);
        return await _leetcode.GetUserStats(felh.Leetcode);
    }

    public async Task<List<RNomination>> ListNominations(int userId)
    {
        var noms = await _context.Ajanlas
            .Include(x => x.Allaskereso)
            .Include(x => x.Allas)
            .ThenInclude(x => x.Ceg)
            .Where(x => x.Allaskeresoid==userId)
            .ToListAsync();
        return noms.ConvertAll(x => new RNomination(x));
    }

    public async Task<int> PendingNominationCount(int userId)
    {
        return await _context.Ajanlas.Where(x => x.Allaskeresoid==userId && x.Jelentkezve==false).CountAsync();
    }

    private string? Mod(string? arg1, string? arg2){
        if(String.IsNullOrEmpty(arg1)){
            return arg2;
        }
        return arg1;
    }
    private long? Mod(long? arg1, long? arg2){
        if(arg1==null){
            return arg2;
        }
        return arg1;
    }

    public async Task Modify(int userId, BUserModify um)
    {
        var instance = await _context.Felhasznalos.SingleAsync(x => x.Id==userId);
        instance.Jelszo=ComputeHash(um.Password) ?? instance.Jelszo;
        instance.Keresztnev=Mod(um.FirstName , instance.Keresztnev);
        instance.Vezeteknev=Mod(um.LastName , instance.Vezeteknev);
        instance.Adoszam=um.TaxNumber ?? instance.Adoszam;
        instance.Anyjaneve=Mod(um.MothersName , instance.Anyjaneve);
        instance.Szuldat=um.BirthDate ?? instance.Szuldat;
        instance.Szulhely=Mod(um.BirthPlace , instance.Szulhely);
        instance.Leetcode=Mod(um.LeetcodeUsername , instance.Leetcode);
        if(um.Vegzettsegek!=null){
            instance.Vegzettsegs.Clear();        
            foreach(var vegz in um.Vegzettsegek){
                Vegzettseg v = new Vegzettseg{
                    Felhasznalo=instance,
                    Rovidleiras=vegz.Rovidleiras,
                    Hosszuleiras=vegz.Hosszuleiras
                };
                instance.Vegzettsegs.Add(v);
            }
        }
        if(um.Competences!=null){
            instance.Felhasznalokompetencia.Clear();
            foreach(var comp in um.Competences){
                var compcount = _context.Kompetencia.Where(x => x.Tipus==comp.Type).Count();
                if(compcount==1){
                    var compinstance = await _context.Kompetencia.SingleAsync(x => x.Tipus==comp.Type);
                    instance.Felhasznalokompetencia.Add(new Felhasznalokompetencium{
                        Kompetencia=compinstance,
                        Szint=comp.Level
                    });
                }
                else{
                    Kompetencium ko = new Kompetencium{
                        Tipus=comp.Type
                    };
                    await _context.AddAsync(ko);
                    instance.Felhasznalokompetencia.Add(new Felhasznalokompetencium{
                            Kompetencia=ko,
                            Szint=comp.Level
                        });
                }
            }
        }        
        await _context.SaveChangesAsync();
    }

    public async Task DocumentUpload(BDokumentumFeltoltes df, int userId)
    {
        using (var memoryStream = new MemoryStream())
        {
            await df.Fajl.CopyToAsync(memoryStream);
            byte[] fileBytes = memoryStream.ToArray();
            // Store the file bytes in the database
            Dokumentum dok = new Dokumentum{
                Leiras=df.Leiras,
                Fajlnev=df.Fajlnev,
                Fajl=fileBytes,
                Felhasznaloid=userId
            };

            _context.Dokumenta.Add(dok);
            await _context.SaveChangesAsync();            
        }
    }

    public async Task<byte[]> DocumentData(int documentId)
    {
        var instance = await _context.Dokumenta.SingleAsync(x => x.Id==documentId);
        return instance.Fajl;
    }

    public async Task<string> DocumentName(int documentId)
    {
        var instance = await _context.Dokumenta.SingleAsync(x => x.Id==documentId);
        return instance.Fajlnev;
    }

    public async Task DeleteDocument(int documentId)
    {
        var instance = await _context.Dokumenta.SingleAsync(x => x.Id==documentId);
        _context.Remove(instance);
        await _context.SaveChangesAsync();
    }
}