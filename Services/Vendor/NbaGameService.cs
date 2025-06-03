using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using basketball_calendar.Models;

namespace basketball_calendar.Services;

/// <summary>
/// Service for retrieving NBA game data from the balldontlie API.
/// </summary>
public class NbaGameService
{
    /// <summary>
    /// Configuration object representing the contents of config.json.
    /// </summary>
    private class Config
    {
        /// <summary>
        /// The API key to use for authenticating requests.
        /// </summary>
        public string api_key { get; set; }
    }

    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://api.balldontlie.io/v1/games";

    /// <summary>
    /// Initializes a new instance of the <see cref="NbaGameService"/> class.
    /// Loads the API key from configuration and sets up the HTTP client with the appropriate authorization header.
    /// </summary>
    public NbaGameService()
    {
        string apiKey = LoadConfig();
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    }

    /// <summary>
    /// Reads the "config.json" file and extracts the API key.
    /// </summary>
    /// <returns>The API key string from the configuration file.</returns>
    private string LoadConfig()
    {
        string json = File.ReadAllText("config.json");
        Config config = JsonSerializer.Deserialize<Config>(json);
        return config.api_key;
    }

    /// <summary>
    /// Retrieves NBA game results for the specified date from the balldontlie API.
    /// </summary>
    /// <param name="date">The date for which to fetch NBA games.</param>
    /// <returns>
    /// A list of <see cref="NbaGame"/> objects representing the games played on the given date.
    /// </returns>
    /// <exception cref="HttpRequestException">
    /// Thrown when the API response indicates a failure (e.g., rate limit exceeded).
    /// </exception>
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
                GameDate = DateTime.Parse(
                    gameJson.GetProperty("date").GetString() ?? string.Empty,
                    CultureInfo.InvariantCulture),
                Status = gameJson.GetProperty("status").GetString() ?? string.Empty,
                HomeTeam = gameJson.GetProperty("home_team").GetProperty("abbreviation").GetString() ?? string.Empty,
                AwayTeam = gameJson.GetProperty("visitor_team").GetProperty("abbreviation").GetString() ?? string.Empty,
                HomeScore = gameJson.GetProperty("home_team_score").GetInt32(),
                AwayScore = gameJson.GetProperty("visitor_team_score").GetInt32()
            };

            games.Add(game);
        }

        return games;
    }
}
