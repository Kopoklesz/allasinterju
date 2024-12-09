using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Allasinterju.Database.Models;

public class RSolveP{
    public int KerdoivId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Language { get; set; }
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

public class RKitoltottP{
    public int KerdoivId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Language { get; set; }
    public string? Codetemplate { get; set; }
    public DateTime KezdesIdo{get;set;}
    public DateTime BefejezesIdo{get;set;}
    public int KitoltesPerc{get;set;}
    public List<RKitoltottPTeszteset> Tesztesetek{get;set;}
    public RKitoltottP(KProgramming kp, bool isUser){
        KerdoivId=kp.Programming.Kerdoivid;
        Title=kp.Programming.Title;
        Description=kp.Programming.Description;
        Language=kp.Programming.Language;
        Codetemplate=kp.Programkod;
        KezdesIdo=(DateTime)kp.Kitoltottkerdoiv.Kitolteskezdet;
        KitoltesPerc= (int)kp.Programming.Kerdoiv.Kitoltesperc;
        BefejezesIdo=KezdesIdo.AddMinutes((int)kp.Programming.Kerdoiv.Kitoltesperc);
        if(isUser==false){
            Tesztesetek=new();
            foreach(var eset in kp.KProgrammingtestcases){
                Tesztesetek.Add(new RKitoltottPTeszteset(eset));
            }
            var elemek = kp.KProgrammingtestcases.Select(x => x.Programmingtestcase);
            foreach(var elem in kp.Programming.Programmingtestcases.Where(x => !elemek.Contains(x))){
                Tesztesetek.Add(new RKitoltottPTeszteset(elem));
            }
        }
    }
    public RKitoltottP(KTobbi kt){
        if(kt.Algorithm!=null){
            var inst = kt.Algorithm;
            KerdoivId=inst.Kerdoiv.Id;
            Title=inst.Title;
            Description=inst.Problemdesc;
            Codetemplate=kt.Szovegesvalasz;            
        }
        else if(kt.Design!=null){
            var inst = kt.Design;
            KerdoivId=inst.Kerdoiv.Id;
            Title=inst.Title;
            Description=inst.Description;
            Codetemplate=kt.Szovegesvalasz;            
        }
        else if(kt.Devops!=null){
            var inst = kt.Devops;
            KerdoivId=inst.Kerdoiv.Id;
            Title=inst.Tasktitle;
            Description=inst.Taskdescription;
            Codetemplate=kt.Szovegesvalasz;            
        }
        else if(kt.Testing!=null){
            var inst = kt.Testing;
            KerdoivId=inst.Kerdoiv.Id;
            Title=inst.Title;
            Description=inst.Taskdesc;
            Codetemplate=kt.Szovegesvalasz;            
        }
        KezdesIdo=(DateTime)kt.Kitoltottkerdoiv.Kitolteskezdet;
        KitoltesPerc= (int)kt.Kitoltottkerdoiv.Kerdoiv.Kitoltesperc;
        BefejezesIdo=KezdesIdo.AddMinutes((int)kt.Kitoltottkerdoiv.Kerdoiv.Kitoltesperc);
    }
}
public class RKitoltottPTeszteset{
    public string Bemenet{get;set;}
    public string ElvartKimenet{get;set;}
    public string? Stdout{get;set;}
    public string? Stderr{get;set;}
    public double? MemoriaMB{get;set;}
    public int? Futasidoms{get;set;}
    public bool? Helyes{get;set;}
    public bool Ellenorizve{get;set;}
    public bool? Nemfutle{get;set;}
    public RKitoltottPTeszteset(KProgrammingtestcase kptc){
        Bemenet=kptc.Programmingtestcase.Input;
        ElvartKimenet=kptc.Programmingtestcase.Output;
        Stdout=kptc.Stdout;
        Stderr=kptc.Stderr;
        MemoriaMB=kptc.Memoria;
        Futasidoms=kptc.Futasido;
        Helyes=kptc.Helyes;
        Nemfutle=kptc.Nemfutle;
        Ellenorizve=kptc.Lefutott;
    }
    public RKitoltottPTeszteset(Programmingtestcase ptc){
        Bemenet=ptc.Input;
        ElvartKimenet=ptc.Output;
        Ellenorizve=false;
    }

}

public class BUserKerdoivIds{
    public int MunkakeresoId{get;set;}
    public int KerdoivId{get;set;}
}