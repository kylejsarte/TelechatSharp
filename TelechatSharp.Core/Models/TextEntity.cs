using System.Text.Json;

namespace TelechatSharp.Core.Models
{
    public class TextEntity
    {
        public string? Type { get; set; }
        public string? Text { get; set; }

        public TextEntity() { }

        internal TextEntity(JsonElement token)
        {
            // TODO: the following code assumes the existence of the "type" and "text" properties. Maybe consider using TryGetProperty instead.
            Type = token.ValueKind == JsonValueKind.String ? "plain" : token.GetProperty("type").GetString();
            Text = token.ValueKind == JsonValueKind.String ? token.GetString() : token.GetProperty("text").GetString();
        }
    }
}