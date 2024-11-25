using System.Text;
using Newtonsoft.Json;

public interface IJudge0Client{

}
public class Judge0Client : IJudge0Client{
    private readonly HttpClient _httpClient;
    private readonly Dictionary<string, int> Languages;
    private readonly string apiUrl="http://localhost:2358";
    public Judge0Client(HttpClient clnt){
        _httpClient = clnt;
        Languages = new Dictionary<string, int>();
        Languages["Bash"]=46;
        Languages["C"]=50;
        Languages["C++"]=54;
        Languages["C#"]=54;
        Languages["Java"]=62;
        Languages["JavaScript"]=63;
        Languages["Python"]=3;
        Languages["TypeScript"]=63;
    }
    public async Task<string> GiveCodeToRun(string code, string input, string language){
        var body = new {
            source_code=code,
            language_id=Languages[language],
            stdin=input
        };
        var jsonData = JsonConvert.SerializeObject(body);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{apiUrl}/submissions?base64_encoded=false", content);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        dynamic responseJson = JsonConvert.DeserializeObject(responseBody);
        return responseJson.token;
    }
}