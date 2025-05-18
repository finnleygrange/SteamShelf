using System.Text.Json.Serialization;

namespace SteamShelf.Model
{
    public class SteamPlayer
    {
        [JsonPropertyName("steamid")]
        public string? SteamId { get; set; }
        [JsonPropertyName("personaname")]
        public string? PersonaName { get; set; }
    }
}
