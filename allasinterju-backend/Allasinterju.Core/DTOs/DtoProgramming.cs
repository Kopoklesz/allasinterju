using Allasinterju.Database.Models;

public class RSolveP{
    public int KerdoivId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Language { get; set; }
    public string? Codetemplate { get; set; }
    public DateTime KezdesIdo{get;set;}
    public DateTime BefejezesIdo{get;set;}
    public int KitoltesPerc{get;set;}
    public RSolveP(Programming p){
        KerdoivId=p.Kerdoivid;
        Title=p.Title;
        Description=p.Description;
        Language=p.Language;
        Codetemplate=p.Codetemplate;
        KezdesIdo=DateTime.Now;
        KitoltesPerc= (int)p.Kerdoiv.Kitoltesperc;
        BefejezesIdo=KezdesIdo.AddMinutes((int)p.Kerdoiv.Kitoltesperc);
    }
    public RSolveP(KProgramming kp){
        KerdoivId=kp.Programming.Kerdoivid;
        Title=kp.Programming.Title;
        Description=kp.Programming.Description;
        Language=kp.Programming.Language;
        Codetemplate=kp.Programkod;
        KezdesIdo=(DateTime)kp.Kitoltottkerdoiv.Kitolteskezdet;
        KitoltesPerc= (int)kp.Programming.Kerdoiv.Kitoltesperc;
        BefejezesIdo=KezdesIdo.AddMinutes((int)kp.Programming.Kerdoiv.Kitoltesperc);
    }
}

public class BSaveProgressP{
    public int KerdoivId{get;set;}
    public string Programkod{get;set;}
}