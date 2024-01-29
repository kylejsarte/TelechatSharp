using System.Text.Json.Serialization;
using TelechatSharp.Core.Constants;
using TelechatSharp.Core.Converters;

namespace TelechatSharp.Core.Models
{
    public class Message
    {
        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("actor")]
        public string? Actor { get; set; }

        [JsonPropertyName("actor_id")]
        public string? ActorId { get; set; }

        [JsonPropertyName("dateunixtime")]
        public string? DateUnixTime { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("duration_seconds")]
        public int? DurationSeconds { get; set; }

        [JsonPropertyName("file")]
        public string? File { get; set; }

        [JsonPropertyName("from")]
        public string? From { get; set; }

        [JsonPropertyName("from_id")]
        public string? FromId { get; set; }

        [JsonPropertyName("height")]
        public int? Height { get; set; }

        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonInclude]
        [JsonPropertyName("text")]
        [JsonConverter(typeof(TextConverter))]
        private Text? _text = default;

        [JsonIgnore]
        public string Text
        {
            get
            {
                return _text?.FullText ?? string.Empty;
            }

            set
            {
                if (_text is not null)
                {
                    _text.FullText = value;
                }
            }
        }

        [JsonPropertyName("text_entities")]
        [JsonConverter(typeof(TextEntityConverter))]
        public IEnumerable<TextEntity>? TextEntities { get; set; }

        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("type")]
        public MessageType? Type { get; set; }

        [JsonPropertyName("sticker_emoji")]
        public string? StickerEmoji { get; set; }

        [JsonPropertyName("media_type")]
        public string? MediaType { get; set; }

        [JsonPropertyName("members")]
        public IEnumerable<string>? Members { get; set; }

        [JsonPropertyName("mime_type")]
        public string? MimeType { get; set; }

        [JsonPropertyName("photo")]
        public string? Photo { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        public Message() { }
    }
}
