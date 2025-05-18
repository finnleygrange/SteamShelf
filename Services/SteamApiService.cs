using SteamShelf.Model;
using System.Text.Json;

namespace SteamShelf.Services
{
    public class SteamApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public SteamApiService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient("SteamApi");
            _apiKey = config["SteamApiKey"];
        }

        private static string ExtractSteamId(string openIdUrl)
        {
            return openIdUrl.Split("/").Last();
        }

        public async Task<SteamPlayer?> GetPlayerAsync(string openId)
        {
            string steamId = ExtractSteamId(openId);
            string url = $"ISteamUser/GetPlayerSummaries/v0002/?key={_apiKey}&steamids={steamId}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SteamApiResponse<SteamPlayerResponseData>>(json);

            return result?.Response.Players.FirstOrDefault();
        }

        public async Task<List<SteamGame>?> GetOwnedGamesAsync(string openId)
        {
            string steamId = ExtractSteamId(openId);
            string url = $"IPlayerService/GetOwnedGames/v0001/?key={_apiKey}&steamid={steamId}&include_appinfo=1&format=json";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SteamApiResponse<SteamOwnedGamesResponseData>>(json);

            return result?.Response.Games;
        }
    }
}
