using Allasinterju.Database.Models;

public class DtoUser{
    public int Id{get;set;}
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string EmailAddress{get;set;}
    public DateTime? BirthDate{get;set;}
    public string? BirthPlace{get;set;}
    public List<DtoJobShort> AppliedJobs{get;set;}
    public DtoUser(Felhasznalo f){
        //INCLUDE: x => x.Kitoltottallas
        //  THENINCLUDE: x => x.Allas
        Id = f.Id;
        FirstName = f.Keresztnev;
        LastName = f.Vezeteknev;
        EmailAddress = f.Email;
        BirthDate = f.Szuldat;
        BirthPlace = f.Szulhely;
        AppliedJobs = new List<DtoJobShort>();
        foreach(var a in f.Kitoltottallas){
            AppliedJobs.Add(new DtoJobShort(a.Allas));
        }
    }
}

public class DtoLogin{
    public string Username{get;set;}
    public string Password{get;set;}
}
public class DtoUserRegister{
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string EmailAddress{get;set;}
    public string Password{get;set;}
    public long? TaxNumber{get;set;}
    public string? MothersName{get;set;}
    public DateTime? BirthDate{get;set;}
    public string? BirthPlace{get;set;}
    public string? InvitationCode{get;set;}
}

public class BUserModify{
    public string FirstName{get;set;}
    public string LastName{get;set;}
    public string Password{get;set;}
    public long? TaxNumber{get;set;}
    public string? MothersName{get;set;}
    public DateTime? BirthDate{get;set;}
    public string? BirthPlace{get;set;}
    public string? LeetcodeUsername{get;set;}
    public List<BCompetence> Competences{get;set;}
    public List<BVegzettseg> Vegzettsegek{get;set;}
}
