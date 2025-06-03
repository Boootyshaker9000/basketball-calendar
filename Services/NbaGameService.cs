using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using basketball_calendar.Models;
namespace basketball_calendar.Services;

/// <summary>
/// Služba pro získávání dat o NBA zápasech z balldontlie API.
/// </summary>
public class NbaGameService
{
    private class Config
    {
        public string api_key { get; set; }
    }
    
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://api.balldontlie.io/v1/games";

    public NbaGameService()
    {   
        string apiKey = LoadConfig();
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }

    private string LoadConfig()
    {
        string json = File.ReadAllText("config.json");
        Config config = JsonSerializer.Deserialize<Config>(json);
        return config.api_key;
    }

    /// <summary>
    /// Získá výsledky NBA zápasů pro zadané datum.
    /// </summary>
    public async Task<List<NbaGame>> GetGamesByDateAsync(DateTime date)
    {
        var formattedDate = date.ToString("yyyy-MM-dd");
        var requestUrl = $"{ApiBaseUrl}?dates[]={formattedDate}&per_page=100";

        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Too many requests. Please, try again later.: {response.StatusCode}");
        }

        var jsonString = await response.Content.ReadAsStringAsync();
        var document = JsonDocument.Parse(jsonString);
        var gamesJson = document.RootElement.GetProperty("data");

        var games = new List<NbaGame>();

        foreach (var gameJson in gamesJson.EnumerateArray())
        {
            var game = new NbaGame
            {
                Id = gameJson.GetProperty("id").GetInt32(),
                GameDate = DateTime.Parse(gameJson.GetProperty("date").GetString() ?? "", CultureInfo.InvariantCulture),
                Status = gameJson.GetProperty("status").GetString() ?? "",
                HomeTeam = gameJson.GetProperty("home_team").GetProperty("abbreviation").GetString() ?? "",
                AwayTeam = gameJson.GetProperty("visitor_team").GetProperty("abbreviation").GetString() ?? "",
                HomeScore = gameJson.GetProperty("home_team_score").GetInt32(),
                AwayScore = gameJson.GetProperty("visitor_team_score").GetInt32()
            };

            games.Add(game);
        }

        return games;
    }
}
