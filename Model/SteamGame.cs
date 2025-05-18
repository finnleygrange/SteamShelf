using System.Text.Json.Serialization;

namespace SteamShelf.Model
{
    public class SteamGame
    {
        [JsonPropertyName("appid")]
        public int AppId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("playtime_2weeks")]
        public int Playtime2Weeks { get; set; }
        [JsonPropertyName("playtime_forever")]
        public int PlaytimeForever {  get; set; }
    }
}
