using Allasinterju.Database.Models;

public class DtoJobShort{
    public int Id{get;set;}
    public string JobTitle{get;set;}
    public string JobType{get;set;}
    public string CompanyName{get;set;}
    public string? City{get;set;}
    public DtoJobShort(Alla a){
        Id = a.Id;
        JobTitle = a.Cim;
        JobType = a.Munkakor;
        CompanyName = a.Ceg.Cegnev;
        City = a.Telephelyszoveg;
    }
}
public class DtoJob{
    public int Id{get;set;}
    public string JobTitle{get;set;}
    public string JobType{get;set;}
    public string? WorkOrder{get;set;}
    public string Description{get;set;}
    public string? ShortDescription{get;set;}
    public string? City{get;set;}
    public DtoCompanyShort Company{get;set;}
    public DtoJob(Alla a){
        //INCLUDE: x => x.Ceg
        //INCLUDE: x => x.Telephely
        Id = a.Id;
        JobTitle = a.Cim;
        JobType = a.Munkakor;
        WorkOrder = a.Munkarend;
        Description = a.Leiras;
        ShortDescription = a.Rovidleiras;
        City = a.Telephelyszoveg;
        Company = new DtoCompanyShort(a.Ceg);
    }
}
public class DtoJobAdd{
    public string JobTitle{get;set;}
    public string JobType{get;set;}
    public string? WorkOrder{get;set;}
    public string Description{get;set;}
    public string? ShortDescription{get;set;}
    public string Location{get;set;}
    public DateTime? Deadline{get;set;}
}