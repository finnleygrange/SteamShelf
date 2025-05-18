using System.Text.Json.Serialization;

namespace SteamShelf.Model
{
    public class SteamPlayer
    {
        [JsonPropertyName("steamid")]
        public string? SteamId { get; set; }
        [JsonPropertyName("personaname")]
        public string? PersonaName { get; set; }
        [JsonPropertyName("avatar")]
        public string? AvatarSmall { get; set; }

        [JsonPropertyName("avatarmedium")]
        public string? AvatarMedium { get; set; }

        [JsonPropertyName("avatarfull")]
        public string? AvatarFull { get; set; }
    }
}
