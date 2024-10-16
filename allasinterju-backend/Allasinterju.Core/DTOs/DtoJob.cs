using Allasinterju.Database.Models;

public class DtoJobShort{
    public int Id{get;set;}
    public string JobTitle{get;set;}
    public string JobType{get;set;}
    public string CompanyName{get;set;}
    public string City{get;set;}
    public DtoJobShort(Allas a){
        Id = a.Id;
        JobTitle = a.Cim;
        JobType = a.Munkakor;
        CompanyName = a.Ceg.Cegnev;
        City = a.Telephely.Telepules;
    }
}