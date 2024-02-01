using System.Text.Json;
using System.Text.Json.Serialization;

namespace TelechatSharp.Core.Models
{
    public class TextEntity
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        public TextEntity() { }

        internal TextEntity(JsonElement token)
        {
            Type = token.ValueKind == JsonValueKind.String ? "plain" : token.GetProperty("type").GetString();
            Text = token.ValueKind == JsonValueKind.String ? token.GetString() : token.GetProperty("text").GetString();
        }
    }
}