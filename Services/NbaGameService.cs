using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using basketball_calendar.Models;

namespace basketball_calendar.Services
{
    /// <summary>
    /// Služba pro získávání dat o NBA zápasech z Python Flask serveru.
    /// </summary>
    public class NbaGameService
    {
        private readonly HttpClient _httpClient;
        private const string PythonApiUrl = "http://localhost:5000/nba_games/";
        private const string FallbackApiUrl = "https://api.balldontlie.io/v1/games";
        private bool _usePythonApi = true;

        public NbaGameService(string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }

        /// <summary>
        /// Získá výsledky NBA zápasů pro konkrétní datum.
        /// </summary>
        /// <param name="date">Datum, pro které chceme získat výsledky.</param>
        /// <returns>Seznam NBA zápasů pro dané datum.</returns>
        public async Task<List<NbaGame>> GetGamesByDateAsync(DateTime date)
        {
            try
            {
                if (_usePythonApi)
                {
                    try
                    {
                        return await GetGamesFromPythonAsync(date);
                    }
                    catch (Exception)
                    {
                        _usePythonApi = false;
                    }
                }

                return await GetGamesDirectlyFromApiAsync(date);
            }
            catch (Exception)
            {
                return new List<NbaGame>();
            }
        }

        /// <summary>
        /// Získá výsledky NBA zápasů přímo z Python API.
        /// </summary>
        private async Task<List<NbaGame>> GetGamesFromPythonAsync(DateTime date)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            string url = $"{PythonApiUrl}{formattedDate}";

            Console.WriteLine($"Volání Python API: {url}");

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Python API vrátilo chybu: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var games = JsonSerializer.Deserialize<List<NbaGame>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<NbaGame>();

            return games;
        }

        /// <summary>
        /// Získá výsledky NBA zápasů přímo z balldontlie API jako fallback řešení.
        /// </summary>
        private async Task<List<NbaGame>> GetGamesDirectlyFromApiAsync(DateTime date)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            string url = $"{FallbackApiUrl}?dates[]={formattedDate}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new List<NbaGame>();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseContent);

            var gamesArray = doc.RootElement.GetProperty("data");
            var games = new List<NbaGame>();

            foreach (var gameElement in gamesArray.EnumerateArray())
            {
                var game = new NbaGame
                {
                    Id = gameElement.GetProperty("id").GetInt32(),
                    GameDate = gameElement.TryGetProperty("date", out var dateElement) ? 
                               DateTime.Parse(dateElement.GetString() ?? DateTime.Now.ToString(), CultureInfo.InvariantCulture) : 
                               DateTime.Now,
                    Status = gameElement.TryGetProperty("status", out var statusElement) ? 
                             statusElement.GetString() ?? "" : 
                             "",
                    HomeTeam = gameElement.GetProperty("home_team").GetProperty("full_name").GetString() ?? "",
                    AwayTeam = gameElement.GetProperty("visitor_team").GetProperty("full_name").GetString() ?? "",
                    HomeScore = gameElement.TryGetProperty("home_team_score", out var homeScoreElement) ? 
                                homeScoreElement.GetInt32() : 0,
                    AwayScore = gameElement.TryGetProperty("visitor_team_score", out var awayScoreElement) ? 
                                 awayScoreElement.GetInt32() : 0
                };

                games.Add(game);
            }

            return games;
        }
    }
}