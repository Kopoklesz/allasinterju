using System.Runtime.InteropServices;
using System.Runtime.Serialization;
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
        CompanyName = a.Ceg.Cegnev+" "+a.Ceg.Cegtipus;
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
    public List<BCompetence> Competences{get;set;}
}

public class DtoSaveProgress{
    public int KerdoivId{get;set;}
    // ez a lista tartalmazza a kérdéseket
    public List<DtoKerdes> Kerdesek{get;set;}
}

public class DtoKerdes{
    // a kérdés id-je
    public int KerdesId{get;set;}
    public bool? Program{get;set;}
    public bool? Kifejtos{get;set;}
    public bool? Kivalasztos{get;set;}
    // amennyiben feleletválasztós, akkor ez a kiválasztott válasz id-je
    public int? KivalasztottValaszId{get;set;}
    // amennyiben programozós, akkor ez az írt kód
    public string? ProgramValasz{get;set;}
    // amennyiben kifejtős, ez az adott válasz
    public string? KifejtosValasz{get;set;}
}

public class DtoKerdoivLetrehozas{
    public string? Nev{get;set;}
    public int Kor{get;set;}
    public int AllasId{get;set;}
    public int? KitoltesPerc{get;set;}
    public List<DtoKerdesLetrehozas> Kerdesek{get;set;}
}

public class DtoKerdesLetrehozas{

    public bool? Kifejtos{get;set;}
    public bool? Program{get;set;}
    public bool? Valasztos{get;set;}
    public string? Szoveg{get;set;}
    public string? ProgramozosAlapszoveg{get;set;}
    public string? Programnyelv{get;set;}
    public List<DtoTesztesetLetrehozas>? Tesztesetek{get;set;}
    public List<DtoKivalasztosLetrehozas>? Valaszok{get;set;}
}

public class DtoTesztesetLetrehozas{
    public string? Bemenet{get;set;}
    public string? Kimenet{get;set;}
}

public class DtoKivalasztosLetrehozas{
    public string ValaszSzoveg{get;set;}
    public bool Helyes{get;set;}
}


public class RDtoKerdoiv{
    public int KerdoivId{get;set;}
    public string? Nev{get;set;}
    public int Kor{get;set;}
    public List<RDtoKerdes> Kerdesek{get;set;}
    public RDtoKerdoiv(Kerdoiv k, bool bizalmasAdat){
        // EZT A KONSTRUKTORT AKKOR KELL HASZNÁLNI, HA ALAP KÉRDŐÍVET KELL ADNI
        // ELÉG HOSSZÚ INCLUDE KELL HOZZÁ
        Nev=k.Nev;
        KerdoivId=k.Id;
        Kor=k.Kor;
        Kerdesek = new List<RDtoKerdes>();
        foreach(var kerdes in k.Kerdes){
            Kerdesek.Add(new RDtoKerdes(kerdes, bizalmasAdat));
        }
    }
}

public class RDtoKerdes{
    public int KerdesId{get;set;}
    public bool? Kifejtos{get;set;}
    public bool? Program{get;set;}
    public bool? Valasztos{get;set;}
    public string? Szoveg{get;set;}
    public List<RDtoTeszteset>? Tesztesetek{get;set;}
    public List<RDtoKivalasztos>? Valaszok{get;set;}
    public RDtoKerdes(Kerde k, bool bizalmasAdat){
        KerdesId=k.Id;
        if(k.Programozos==true){
            Szoveg=k.Programalapszoveg;
            Program=true;   
            if(bizalmasAdat==true){
                Tesztesetek=new List<RDtoTeszteset>();
                foreach(var te in k.Tesztesets){
                    Tesztesetek.Add(new RDtoTeszteset(te)
                    
                    );
                }
            }         
        }
        if(k.Kifejtos==true){
            Kifejtos=true;
        }
        if(k.Feleletvalasztos==true){
            Valasztos=true;
            Valaszok=new List<RDtoKivalasztos>();
            foreach(var valasz in k.Valaszs){
                Valaszok.Add(new RDtoKivalasztos(valasz, bizalmasAdat));
            }
        }
    }
}
public class RDtoTeszteset{
    public int Id{get;set;}
    public string Bemenet{get;set;}
    public string Kimenet{get;set;}
    public RDtoTeszteset(Teszteset te){
        Id=te.Id;
        Bemenet=te.Bemenet ?? "";
        Kimenet=te.Kimenet ?? "";
    }
}

public class RDtoKivalasztos{
    public int ValaszId{get;set;}
    public string ValaszSzoveg{get;set;}
    public bool? Helyes{get;set;}
    public bool? Kivalasztott{get;set;}
    public RDtoKivalasztos(Valasz v, bool bizalmasAdat){
        ValaszId = v.Id;
        ValaszSzoveg = v.Szoveg;
        if(bizalmasAdat==true){
            Helyes=v.Helyes;
        }
    }
}


public class RDtoKitoltottKerdoiv{
    public int FelhasznaloId{get;set;}
    public int KitoltottKerdoivId{get;set;}
    public int? Pontszam{get;set;}
    public int? Maxpont{get;set;}
    public bool? ProgramHelyes{get;set;}
    public bool? Tovabbjut{get;set;}
    public bool? MIajanlat{get;set;}
}

public class RDtoKerdoivShort{
    public int Id{get;set;}
    public string? Nev{get;set;}
    public int? KitoltesPerc{get;set;}
    public int? Kor{get;set;}
    public bool? Kifejtos{get;set;}
    public bool? Programozos{get;set;}
    public bool? Valasztos{get;set;}
    public RDtoKerdoivShort(Kerdoiv k){
        Id=k.Id;
        Nev=k.Nev;
        KitoltesPerc=k.Kitoltesperc;
        Kor=k.Kor;
        Kifejtos=k.Kerdes.First().Kifejtos;
        Programozos=k.Kerdes.First().Programozos;
        Valasztos=k.Kerdes.First().Feleletvalasztos;
    }
}

public class RRoundSummary{
    public int KerdoivId{get;set;}
    public string KerdoivNev{get;set;}
    public string Tipus{get;set;}
    public List<RRoundUserSummaryShort> Kitoltesek{get;set;}
    public RRoundSummary(Kerdoiv k){
        KerdoivId=k.Id;
        KerdoivNev=k.Nev;
        if(k.Programming){
            Tipus="programming";
        }
        else if(k.Design){
            Tipus="design";
        }
        else if(k.Algorithm){
            Tipus="algorithm";
        }
        else if(k.Testing){
            Tipus="testing";
        }
        else if(k.Devops){
            Tipus="devops";
        }
        Kitoltesek=new();
        foreach(var kk in k.Kitoltottkerdoivs){
            Kitoltesek.Add(new RRoundUserSummaryShort(kk));
        }
    }
}

public class RRoundUserSummaryShort{
    public int KitoltottKerdoivId{get;set;}
    public int MunkakeresoId{get;set;}
    public string Vezeteknev{get;set;}
    public string Keresztnev{get;set;}
    public double? Szazalek{get;set;}
    public double? MIszazalek{get;set;}
    public bool? Tovabbjut{get;set;}
    public bool? MIajanlas{get;set;}
    public RRoundUserSummaryShort(Kitoltottkerdoiv kk){
        KitoltottKerdoivId=kk.Id;
        MunkakeresoId=kk.Kitoltottallas.Allaskeresoid;
        Vezeteknev=kk.Kitoltottallas.Allaskereso.Vezeteknev;
        Keresztnev=kk.Kitoltottallas.Allaskereso.Keresztnev;
        Szazalek=kk.Szazalek;
        MIszazalek=kk.Miszazalek;
        Tovabbjut=kk.Tovabbjut;
        MIajanlas=kk.Miajanlas;
    }
}