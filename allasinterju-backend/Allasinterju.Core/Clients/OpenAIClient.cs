using System.Text;
using System.Text.RegularExpressions;
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
    string TestKey();
}

public class OpenAIClient : IOpenAIClient
{
    private readonly HttpClient _httpClient;
    private readonly string apiKey;
    private readonly string apiUrl = "https://api.openai.com/v1/chat/completions";
    public OpenAIClient(IConfiguration configuration, HttpClient clnt)
    {
        _httpClient=clnt;
        apiKey = configuration["OpenAI:ApiKey"] ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");
    }
    public string TestKey(){
        return apiKey;
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
            messages = new[]{
                new{role = "user", content = prompt}
            },
            max_tokens = 750
        };

        var jsonContent = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(apiUrl, content);
        if(!response.IsSuccessStatusCode){
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error: {response.StatusCode}, Content: {errorContent}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
        return responseString;
    }

    private List<OpenAIResponse> ParseJsonResponse(string jsonResponse)
    {
        try
        {
            var jsonResponseObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);

            // Extract the "choices" array
            var choices = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonResponseObject["choices"].ToString());

            var responseList = new List<OpenAIResponse>();

            foreach (var choice in choices)
            {
                // Check if the message key exists before attempting to parse
                if (choice.ContainsKey("message"))
                {
                    var message = JsonConvert.DeserializeObject<Dictionary<string, object>>(choice["message"].ToString());

                    // Ensure the "content" key exists
                    if (message.ContainsKey("content"))
                    {
                        var content = message["content"].ToString();
                        
                        // Clean up the content (trim unnecessary characters and extra whitespace)
                        content = content.Trim('`', '\n', ' ');

                        // Clean up any unwanted text (if there's a pattern you want to match, adjust the regex)
                        int jsonStartIndex = content.IndexOf('['); // Assumes JSON starts with a list
                        if (jsonStartIndex >= 0)
                        {
                            content = content.Substring(jsonStartIndex);  // Keep only the part after the JSON array starts
                        }

                        // Deserialize the cleaned content into a list of OpenAIResponse objects
                        try
                        {
                            var parsedResponses = JsonConvert.DeserializeObject<List<OpenAIResponse>>(content);
                            responseList.AddRange(parsedResponses);  // Add to the main list
                        }
                        catch (JsonException jsonEx)
                        {
                            Console.WriteLine($"Error deserializing response content: {jsonEx.Message}");
                        }
                    }
                }
            }

            return responseList;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
            return new List<OpenAIResponse>();  // Return an empty list on failure
        }
    }
}