using Newtonsoft.Json;
public interface ILeetcodeClient{
    public Task<LeetcodeResponse> GetUserStats(string username);
}
public class LeetcodeClient : ILeetcodeClient{
    private readonly HttpClient _httpClient;
    private readonly string apiUrl="https://leetcode-stats-api.herokuapp.com";
    public LeetcodeClient(HttpClient clnt){
        _httpClient=clnt;
    }
    public async Task<LeetcodeResponse> GetUserStats(string username){
        // https://leetcode-stats-api.herokuapp.com/neal_wu
        var resp = await _httpClient.GetAsync($"{apiUrl}/{username}");
        resp.EnsureSuccessStatusCode();
        var respBody = await resp.Content.ReadAsStringAsync();
        dynamic result = JsonConvert.DeserializeObject(respBody);
        LeetcodeResponse stats = new LeetcodeResponse{
            Success=result.status=="success" ? true : false,
            TotalSolved=result.totalSolved,
            TotalQuestions=result.totalQuestions,
            EasySolved=result.easySolved,
            TotalEasy=result.totalEasy,
            MediumSolved=result.mediumSolved,
            TotalMedium=result.totalMedium,
            HardSolved=result.hardSolved,
            TotalHard=result.totalHard
        };
        return stats;
    }
}

public class LeetcodeResponse{
    public bool Success{get;set;}
    public int? TotalSolved{get;set;}
    public int? TotalQuestions{get;set;}
    public int? EasySolved{get;set;}
    public int? TotalEasy{get;set;}
    public int? MediumSolved{get;set;}
    public int? TotalMedium{get;set;}
    public int? HardSolved{get;set;}
    public int? TotalHard{get;set;}
}