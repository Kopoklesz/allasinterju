public class BDevOpsAdd{
    public int JobId{get;set;}
    public string Name{get;set;}
    public int Round{get;set;}
    public string Title{get;set;}
    public string Category{get;set;}
    public string Difficulty{get;set;}
    public string Description{get;set;}
    public int TimeLimit{get;set;}
    public string Platform{get;set;}
    public string SystemRequirements{get;set;}
    public string ResourceLimits{get;set;}
    public string AccessRequirements{get;set;}
    public string ArchitectrueDescription{get;set;}
    public string InfrastructureConstraints{get;set;}
    public bool DocumentationRequired{get;set;}
    public string? DocumentationFormat{get;set;}
    public List <BPrerequisites> Prerequisites{get;set;}
    public List <BTask> Tasks{get;set;}
    public List<BComponent> Components{get;set;}
    public List<BDeliverables> Deliverables{get;set;}
    public List<BEvalCriteriaDevops> EvaluationCriteria{get;set;}
    public List<BDocumentationTemplate> DocTemplates{get;set;}
}
public class BPrerequisites{
    public string Tool{get;set;}
    public string Version{get;set;}
    public string Purpose{get;set;}
}
public class BTask{
    public string Title{get;set;}
    public string Description{get;set;}
    public List<string> Steps{get;set;}
    public string Validation{get;set;}
    public int Points{get;set;}
}
public class BComponent{
    public string Name{get;set;}
    public string Type{get;set;}
    public string Configuration{get;set;}
}
public class BDeliverables{
    public string Title{get;set;}
    public string Description{get;set;}
    public string Acceptance{get;set;}
    public string Format{get;set;}
}
public class BEvalCriteriaDevops{
    public string Title{get;set;}
    public double Weight{get;set;}
    public string Description{get;set;}
}
public class BDocumentationTemplate{
    public string Title{get;set;}
    public string Content{get;set;}
    public bool Required{get;set;}
}