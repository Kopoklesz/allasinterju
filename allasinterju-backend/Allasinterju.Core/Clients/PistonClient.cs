using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

public class LangParam{
    public string Main{get;set;}
    public string Name{get;set;}
    public string Version{get;set;}
}
public class PistonResponse{
    public PRRun Run{get;set;}
}
public class PRRun{
    public string Stdout{get;set;}
    public string Stderr{get;set;}
    public int Code{get;set;}
    public int Memory{get;set;}
    public int Cpu_time{get;set;}
}
public interface IPistonClient{
    
}
public class PistonClient : IPistonClient{
    private readonly HttpClient _httpClient;
    private string apiUrl="http://localhost:2000";
    private Dictionary<string, LangParam> languages;
    public PistonClient(HttpClient clnt){
        _httpClient=clnt;
        languages = new();
        languages["C"]=new LangParam{
            Main="main.c",
            Name="c",
            Version="10.2.0"
        };
        languages["C++"]=new LangParam{
            Main="main.cpp",
            Name="c++",
            Version="10.2.0"
        };
        languages["C#"]=new LangParam{
            Main="Program.cs",
            Name="csharp.net",
            Version="5.0.201"
        };
        languages["Python"]=new LangParam{
            Main="main.py",
            Name="python",
            Version="3.12.0"
        };
        languages["Java"]=new LangParam{
            Main="main.java",
            Name="java",
            Version="15.0.2"
        };
        languages["JavaScript"]=new LangParam{
            Main="main.js",
            Name="javascript",
            Version="20.11.1"
        };
    }
}