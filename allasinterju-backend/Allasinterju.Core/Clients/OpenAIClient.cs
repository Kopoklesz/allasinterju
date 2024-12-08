using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
public class OpenAIResponse
{
    public int Id { get; set; }
    public double Szazalek { get; set; }
    public bool Tovabbjut { get; set; }
}

public interface IOpenAIClient
{
    Task<List<OpenAIResponse>> RunPrompt(string prompt);
}

public class OpenAIClient : IOpenAIClient
{
    private readonly HttpClient _httpClient;
    private readonly string apiKey;
    private readonly string apiUrl = "https://api.openai.com/v1/completions";
    public OpenAIClient(IConfiguration configuration, HttpClient clnt)
    {
        _httpClient=clnt;
        apiKey = configuration["OpenAI:ApiKey"] ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");
    }

    public async Task<List<OpenAIResponse>> RunPrompt(string prompt)
    {
        var responseJson = await GetOpenAIResponse(prompt);
        return ParseJsonResponse(responseJson);
    }

    private async Task<string> GetOpenAIResponse(string prompt)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        
        var requestBody = new
        {
            model = "gpt-4o-mini",
            prompt = prompt,
            max_tokens = 750
        };

        var jsonContent = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(apiUrl, content);
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        return responseString;
    }

    private List<OpenAIResponse> ParseJsonResponse(string jsonResponse)
    {
        try
        {
            var jsonResponseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
            var choices = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonResponseObject["choices"].ToString());
            
            var responseList = new List<OpenAIResponse>();
            foreach(var choice in choices)
            {
                var text = choice["text"];
                var responses = JsonConvert.DeserializeObject<List<OpenAIResponse>>(text);
                responseList.AddRange(responses);
            }
            return responseList;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
            return new List<OpenAIResponse>();
        }
    }
}