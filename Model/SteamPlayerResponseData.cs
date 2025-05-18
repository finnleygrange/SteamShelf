using System.Text.Json.Serialization;

namespace SteamShelf.Model
{
    public class SteamPlayerResponseData
    {
        [JsonPropertyName("players")]
        public List<SteamPlayer> Players { get; set; } = new();
    }
}
