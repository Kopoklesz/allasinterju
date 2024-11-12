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