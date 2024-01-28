using System.Text.Json.Serialization;
using TelechatSharp.Core.Constants;
using TelechatSharp.Core.Converters;

namespace TelechatSharp.Core.Models
{
    public class Message
    {
        public string? Action { get; set; }

        public string? Actor { get; set; }

        [JsonPropertyName("actor_id")]
        public string? ActorId { get; set; }

        public string? DateUnixTime { get; set; }

        public DateTime? Date { get; set; }

        [JsonPropertyName("duration_seconds")]
        public int? DurationSeconds { get; set; }

        public string? File { get; set; }

        public string? From { get; set; }

        [JsonPropertyName("from_id")]
        public string? FromId { get; set; }

        public int? Height { get; set; }

        public long? Id { get; set; }

        [JsonInclude]
        [JsonPropertyName("text")]
        [JsonConverter(typeof(TextConverter))]
        private Text? _text = default;

        [JsonIgnore]
        public string? Text
        {
            get
            {
                return _text?.FullText;
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

        public string? Thumbnail { get; set; }

        public string? Title { get; set; }

        public MessageType? Type { get; set; }

        [JsonPropertyName("sticker_emoji")]
        public string? StickerEmoji { get; set; }

        [JsonPropertyName("media_type")]
        public string? MediaType { get; set; }

        public IEnumerable<string>? Members { get; set; }

        [JsonPropertyName("mime_type")]
        public string? MimeType { get; set; }

        public string? Photo { get; set; }

        public int? Width { get; set; }

        public Message() { }
    }
}
