using System.Security;
using Allasinterju.Database.Models;
using Microsoft.Identity.Client;

public class DtoCompanyShort{
    public int Id{get;set;}
    public string CompanyName{get;set;}
    public string CompanyType{get;set;}
    public DtoCompanyShort(Ceg c){
        Id = c.Id;
        CompanyName = c.Cegnev;
        CompanyType = c.Cegtipus;
    }
}
public class DtoCompany{
    public int Id{get;set;}
    public string CompanyName{get;set;}
    public string CompanyType{get;set;}
    public string? Description{get;set;}
    public string? MainAddress{get;set;}
    public string? MailingAddress{get;set;}
    public string? OutsideCommunicationsEmployee{get;set;}
    public string? MobilePhoneNumber{get;set;}
    public string? CablePhoneNumber{get;set;}
    public string? PictureBase64{get;set;}
    public List<DtoJobShort> Jobs{get;set;}
    public DtoCompany(Ceg c){
        // INCLUDE: x => x.Allas, THENINCLUDE: x => x.Telephely
        Id = c.Id;
        CompanyName = c.Cegnev;
        CompanyType = c.Cegtipus;
        Description = c.Leiras;
        MainAddress = c.Telephely;
        MailingAddress = c.Levelezesicim;
        OutsideCommunicationsEmployee = c.Kapcsolattarto;
        MobilePhoneNumber = c.Mobiltelefon;
        CablePhoneNumber = c.Telefon;
        PictureBase64 = c.Kep!=null ? Convert.ToBase64String(c.Kep) : null;
        Jobs = new List<DtoJobShort>();
        foreach(var a in c.Allas){
            Jobs.Add(new DtoJobShort(a));
        }
    }
}
public class DtoCompanyRegister{
    public string Email{get;set;}
    public string Password{get;set;}
    public string CompanyName{get;set;}
    public string CompanyType{get;set;}
    public string? Description{get;set;}
    public DtoPlace? Place{get;set;}
    public string? MailingAddress{get;set;}
    public string? HREmployee{get;set;}
    public string? MobilePhoneNumber{get;set;}
    public string? CablePhoneNumber{get;set;}
    public string? PictureBase64{get;set;}

}
public class DtoInvitation{
    public string Code{get;set;}
    public DateTime? Expiration{get;set;}
}
public class RDtoInvitation{
    public int Id{get;set;}
    public string Code{get;set;}
    public DateTime? Expiration{get;set;}
    public RDtoInvitation(Meghivokod m){
        Id=m.Id;
        Code=m.Kod;
        Expiration=m.Ervenyesseg;
    }
}