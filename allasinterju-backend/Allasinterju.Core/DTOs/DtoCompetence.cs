using Allasinterju.Database.Models;

public class RDtoCompetence{
    public int Id{get;set;}
    public string Type{get;set;}
    public RDtoCompetence(Kompetencium k){
        Id=k.Id;
        Type=k.Tipus;
    }
}

public class DtoCompetenceJob{
    public string Type{get;set;}
    public int JobId{get;set;}
}

public class DtoCompetenceJobDelete{
    public int Id{get;set;}
    public int JobId{get;set;}
}