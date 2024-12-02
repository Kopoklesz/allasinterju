public class BProgrammingAdd{
    public int JobId{get;set;}
    public string Name{get;set;}
    public int Round{get;set;}
    public string Title{get;set;}
    public string Description{get;set;}
    public string Language{get;set;}
    public int TimeLimit{get;set;}
    public string CodeTemplate{get;set;}
    public List<BTestCase>? TestCases{get;set;}

}
public class BTestCase{
    public string Input{get;set;}
    public string ExpectedOutput{get;set;}
}
