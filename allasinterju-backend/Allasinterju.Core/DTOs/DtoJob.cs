using System.Runtime.InteropServices;
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

public class RDtoKerdoiv{
    public int KerdoivId{get;set;}
    public List<RDtoKerdes> Kerdesek{get;set;}
    public RDtoKerdoiv(Kerdoiv k, bool bizalmasAdat){
        // EZT A KONSTRUKTORT AKKOR KELL HASZNÁLNI, HA ALAP KÉRDŐÍVET KELL ADNI
        // ELÉG HOSSZÚ INCLUDE KELL HOZZÁ
        KerdoivId=k.Id;
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
    public string? TesztesetJson{get;set;}
    public List<RDtoKivalasztos>? Valaszok{get;set;}
    public RDtoKerdes(Kerde k, bool bizalmasAdat){
        KerdesId=k.Id;
        if(k.Programozos==true){
            Szoveg=k.Programalapszoveg;
            Program=true;   
            if(bizalmasAdat==true){
                TesztesetJson=k.Programteszteset;
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

