using System.Text.Json.Serialization;

namespace SteamShelf.Model
{
    public class SteamApiResponse<T>
    {
        [JsonPropertyName("response")]
        public T Response { get; set; } = default!;
    }
}
