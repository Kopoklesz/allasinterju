using System.Linq.Expressions;
using Allasinterju.Database.Models;

public class RMunkakeresoShort{
    public int Id{get;set;}
    public string Keresztnev{get;set;}
    public string Vezeteknev{get;set;}
    public List<RCompetence> Competences{get;set;}
    public RMunkakeresoShort(Felhasznalo f){
        Id=f.Id;
        Keresztnev=f.Keresztnev;
        Vezeteknev=f.Vezeteknev;
        Competences=new List<RCompetence>();
        foreach(var fk in f.Felhasznalokompetencia){
            Competences.Add(new RCompetence(fk));
        }
    }
}

public class RMunkakereso{
    public int Id{get;set;}
    public string Keresztnev{get;set;}
    public string Vezeteknev{get;set;}
    public List<RCompetence> Kompetenciak{get;set;}
    public List<RVegzettseg> Vegzettsegek{get;set;}
    public List<RFajl> Fajlok{get;set;}
    public List<RAllasKitoltes> KitoltottAllasok{get;set;}
    public LeetcodeResponse? LeetcodeStatisztika{get;set;}
    public RMunkakereso(Felhasznalo f){
        Id=f.Id;
        Keresztnev=f.Keresztnev;
        Vezeteknev=f.Vezeteknev;
        Kompetenciak=new();
        Vegzettsegek=new();
        Fajlok=new();
        KitoltottAllasok=new();
        foreach(var komp in f.Felhasznalokompetencia){
            Kompetenciak.Add(new RCompetence(komp));
        }
        foreach(var vegz in f.Vegzettsegs){
            Vegzettsegek.Add(new RVegzettseg(vegz));
        }
        foreach(var fajl in f.Dokumenta){
            Fajlok.Add(new RFajl(fajl));
        }
        foreach(var ka in f.Kitoltottallas){
            KitoltottAllasok.Add(new RAllasKitoltes(ka));
        }
    }
}
public class RVegzettseg{
    public string Rovidleiras{get;set;}
    public string? Hosszuleiras{get;set;}
    public RVegzettseg(Vegzettseg v){
        Rovidleiras=v.Rovidleiras;
        Hosszuleiras=v.Hosszuleiras;
    }
}
public class RFajl{
    public int Id{get;set;}
    public string Leiras{get;set;}
    public string Fajlnev{get;set;}
    public RFajl(Dokumentum d){
        Id=d.Id;
        Leiras=d.Leiras;
        Fajlnev=d.Fajlnev;
    }
}
public class RAllasKitoltes{
    public string Allascim{get;set;}
    public string Cegnev{get;set;}
    public List<RKerdoivKitoltes> KitoltottKerdoivek{get;set;}
    public RAllasKitoltes(Kitoltottalla ka){
        Allascim=ka.Allas.Cim;
        Cegnev=ka.Allas.Ceg.Cegnev+" "+ka.Allas.Ceg.Cegtipus;
        KitoltottKerdoivek=new List<RKerdoivKitoltes>();
        foreach(var kk in ka.Kitoltottkerdoivs){
            KitoltottKerdoivek.Add(new RKerdoivKitoltes(kk));
        }
    }
}
public class RKerdoivKitoltes{
    public string Kerdoivnev{get;set;}
    public double? Szazalek{get;set;}
    public bool TovabbjutasEldontve{get;set;}
    public bool? Tovabbjutott{get;set;}
    public RKerdoivKitoltes(Kitoltottkerdoiv kk){
        Kerdoivnev=kk.Kerdoiv.Nev;
        Szazalek=kk.Szazalek;
        TovabbjutasEldontve=kk.Tovabbjut!=null ? true : false;
        Tovabbjutott=kk.Tovabbjut;
    }
}

