using System.ComponentModel;
using Microsoft.Identity.Client;

public class BDesignAdd{
    public int JobId{get;set;}
    public string Name{get;set;}
    public int Round{get;set;}
    public string Title{get;set;}
    public string Category{get;set;}
    public string Description{get;set;}
    public int TimeLimit{get;set;}
    public List<BRequirement> DesignRequirements{get;set;}
    public string StyleGuide{get;set;}
    public string Deliverables{get;set;}
    public List<BRefLinks> ReferenceLinks{get;set;}
    public List<BEvalCriteria> EvalCriteria{get;set;}
    
}
public class BRequirement{
    public string Category{get;set;}
    public string Description{get;set;}
}
public class BRefLinks{
    public string Description{get;set;}
    public string Url{get;set;}
}
public class BEvalCriteria{
    public string Description{get;set;}
    public double Weight{get;set;}
}