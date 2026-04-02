using Allasinterju.Database.Models;

public class RDtoCompetence{
    public int Id{get;set;}
    public string Type{get;set;}
    public string Level{get;set;}
    public RDtoCompetence(Kompetencium k){
        Id=k.Id;
        Type=k.Tipus;
    }
    public RDtoCompetence(Allaskompetencium ak){
        Id=ak.Kompetenciaid;
        Type=ak.Kompetencia.Tipus;
        Level=ak.Szint;
    }
    public RDtoCompetence(Felhasznalokompetencium fk){
        Id=fk.Kompetenciaid;
        Type=fk.Kompetencia.Tipus;
        Level=fk.Szint;
    }
}

public class RDtoCompetenceOnly{
    public int Id{get;set;}
    public string Type{get;set;}
    public RDtoCompetenceOnly(Kompetencium k){
        Id=k.Id;
        Type=k.Tipus;
    }
}

public class DtoCompetenceJob{
    public string Type{get;set;}
    public string Level{get;set;}
    public int JobId{get;set;}
}

public class DtoCompetenceJobDelete{
    public int Id{get;set;}
    public int JobId{get;set;}
}
public class BCompetence{
    public string Type{get;set;}
    public string Level{get;set;}
}
public class RCompetence{
    public string Type{get;set;}
    public string Level{get;set;}
    public RCompetence(Felhasznalokompetencium k){
        Type=k.Kompetencia.Tipus;
        Level=k.Szint;
    }
}