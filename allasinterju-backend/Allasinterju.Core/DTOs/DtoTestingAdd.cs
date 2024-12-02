using System.ComponentModel;

public class BTestingAdd{
    public int JobId{get;set;}
    public string Name{get;set;}
    public int Round{get;set;}
    public string Title{get;set;}
    public string Description{get;set;}
    public string Type{get;set;}
    public int TimeLimit{get;set;}
    public string AppUrl{get;set;}
    public string OS{get;set;}
    public string Browser{get;set;}
    public string Resolution{get;set;}
    public string AdditionalRequirements{get;set;}
    public string StepsToReproduce{get;set;}
    public string ExpectedResult{get;set;}
    public string ActualResult{get;set;}
    public string Severity{get;set;}
    public string Priority{get;set;}
    public bool RequireAttachments{get;set;}
    public List<BTool> Tools{get;set;}
    public List<BTestScenario> Scenarios{get;set;}
    public List<BTestCaseTesting> TestCases{get;set;}
    public List<BEvalCriteriaTesting> EvaluationCriteria{get;set;}

}
public class BTool{
    public string Name{get;set;}
    public string? Version{get;set;}
    public string? Purpose{get;set;}
}
public class BTestScenario{
    public string Title{get;set;}
    public string Description{get;set;}
    public string Preconditions{get;set;}
    public string Priority{get;set;}
}
public class BTestCaseTesting{
    public string RelatedScenario{get;set;}
    public string Title{get;set;}
    public List<string> Steps{get;set;}
    public string ExpectedResult{get;set;}
    public string TestData{get;set;}
    public bool CanBeAutomated{get;set;}
    public int Points{get;set;}
}
public class BEvalCriteriaTesting{
    public string Title{get;set;}
    public double Weight{get;set;}
    public string Description{get;set;}
}
