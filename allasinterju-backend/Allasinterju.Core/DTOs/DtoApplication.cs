using Allasinterju.Database.Models;
using Microsoft.Identity.Client;

public class RApplicationShort{
    public int MunkakeresoId{get;set;}
    public string Vezeteknev{get;set;}
    public string Keresztnev{get;set;}
    public double? Vegsoszazalek{get;set;}
    public RApplicationShort(Kitoltottalla ka){
        MunkakeresoId=ka.Allaskeresoid;
        Vezeteknev=ka.Allaskereso.Vezeteknev;
        Keresztnev=ka.Allaskereso.Keresztnev;
    }
}
public class RApplication{
    public int MunkakeresoId{get;set;}
    public string Vezeteknev{get;set;}
    public string Keresztnev{get;set;}
    public List<RApplicationRound> Fordulok{get;set;}
    public double? Vegsoszazalek{get;set;} //TODO
    public RApplication(Kitoltottalla ka){
        MunkakeresoId=ka.Allaskeresoid;
        Vezeteknev=ka.Allaskereso.Vezeteknev;
        Keresztnev=ka.Allaskereso.Keresztnev;
        Fordulok=new List<RApplicationRound>();
        foreach(var kk in ka.Kitoltottkerdoivs){
            Fordulok.Add(new RApplicationRound(kk));
        }
    }
}
public class RApplicationRound{
    public int KerdoivId{get;set;}
    public int KitoltottKerdoivId{get;set;}
    public int Kor{get;set;}
    public string KerdoivNev{get;set;}
    public double? Szazalek{get;set;}
    public bool? Tovabbjut{get;set;}
    public RApplicationRound(Kitoltottkerdoiv kk){
        KitoltottKerdoivId=kk.Id;
        KerdoivId=kk.Kerdoiv.Id;
        Kor=kk.Kerdoiv.Kor;
        KerdoivNev=kk.Kerdoiv.Nev;
        Szazalek=kk.Szazalek;
        Tovabbjut=kk.Tovabbjut;
    }
}

public class BApplication{
    public int JobId{get;set;}
    public int MunkakeresoId{get;set;}
}