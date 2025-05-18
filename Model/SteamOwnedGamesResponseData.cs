using System.Text.Json.Serialization;

namespace SteamShelf.Model
{
    public class SteamOwnedGamesResponseData
    {
        [JsonPropertyName("game_count")]
        public int GameCount { get; set; }
        [JsonPropertyName("games")]
        public List<SteamGame> Games { get; set; } = new();
    }
}
