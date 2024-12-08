using Allasinterju.Database.Models;

public class RRound{
    public int KerdoivId{get;set;}
    public string Nev{get;set;}
    public int Kor{get;set;}
    public string Tipus{get;set;}
    public RRound(Kerdoiv k){
        KerdoivId=k.Id;
        Nev=k.Nev;
        Kor=k.Kor;
        if(k.Programming){
            Tipus="programming";
        }
        if(k.Testing){
            Tipus="testing";
        }
        if(k.Algorithm){
            Tipus="algorithm";
        }
        if(k.Design){
            Tipus="design";
        }
        if(k.Devops){
            Tipus="devops";
        }
    }
}

public class BEvalAI{
    public int KerdoivId{get;set;}
    public int JeloltSzam{get;set;}
    public string TovabbiPromptBemenet{get;set;}
}