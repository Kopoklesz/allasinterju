public class BAlgorithmAdd{
    public int JobId{get;set;}
    public string Name{get;set;}
    public int Round{get;set;}
    public string Title{get;set;}
    public string Category{get;set;}
    public string Difficulty{get;set;}
    public int TimeLimit{get;set;}
    public string Description{get;set;}
    public string InputFormat{get;set;}
    public string OutputFormat{get;set;}
    public string TimeComplexity{get;set;}
    public string SpaceComplexity{get;set;}
    public string SampleSolution{get;set;}
    public List<string> Constraints{get;set;}
    public List<BExample> Examples{get;set;}
    public List<string> Hints{get;set;}
    public List<BTestCaseAlgorithm> TestCases{get;set;}

}
public class BExample{
    public string Input{get;set;}
    public string Output{get;set;}
    public string Explanation{get;set;}
}
public class BTestCaseAlgorithm{
    public string Input{get;set;}
    public string Output{get;set;}
    public bool Hidden{get;set;}
    public int Points{get;set;}
}