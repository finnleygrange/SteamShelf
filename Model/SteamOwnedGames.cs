using System.Text.Json.Serialization;

namespace SteamShelf.Model
{
    public class SteamOwnedGames
    {
        [JsonPropertyName("response")]
        public SteamOwnedGamesResponseData? Response { get; set; }
    }
}
