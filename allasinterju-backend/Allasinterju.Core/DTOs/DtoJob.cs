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
public class DtoJob{
    public int Id{get;set;}
    public string JobTitle{get;set;}
    public string JobType{get;set;}
    public string? WorkOrder{get;set;}
    public string Description{get;set;}
    public string? ShortDescription{get;set;}
    public string City{get;set;}
    public string ExactLocation{get;set;}
    public DtoCompanyShort Company{get;set;}
    public DtoJob(Allas a){
        //INCLUDE: x => x.Ceg
        //INCLUDE: x => x.Telephely
        Id = a.Id;
        JobTitle = a.Cim;
        JobType = a.Munkakor;
        WorkOrder = a.Munkarend;
        Description = a.Leiras;
        ShortDescription = a.Rovidleiras;
        City = a.Telephely.Telepules;
        var telephely = a.Telephely;
        ExactLocation = telephely.Irsz+" ";
        ExactLocation += telephely.Telepules+" ";
        ExactLocation += telephely.Utcahazszam;
        ExactLocation = ExactLocation.Trim();
        Company = new DtoCompanyShort(a.Ceg);
    }
}