using System.Text.Json.Serialization;
using TelechatSharp.Core.Constants;
using TelechatSharp.Core.Converters;

namespace TelechatSharp.Core.Models
{
    public class Message
    {
        [JsonPropertyName("action")]
        public string? Action { get; set; }

        private string _actor = string.Empty;

        [JsonPropertyName("actor")]
        public string Actor
        {
            get => string.IsNullOrEmpty(_actor) ? _from : _actor;
            set => _actor = value;
        }

        private string _actorId = string.Empty;

        [JsonPropertyName("actor_id")]
        public string ActorId
        {
            get => string.IsNullOrEmpty(_actorId) ? _fromId : _actorId;
            set => _actorId = value;
        }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("dateunixtime")]
        public string? DateUnixTime { get; set; }

        [JsonPropertyName("duration")]
        public int? Duration { get; set; }

        [JsonPropertyName("duration_seconds")]
        public int? DurationSeconds { get; set; }

        [JsonPropertyName("edited")]
        public DateTime? Edited { get; set; }

        [JsonPropertyName("edited_unixtime")]
        public string EditedUnixTime { get; set; }

        [JsonPropertyName("file")]
        public string? File { get; set; }

        [JsonPropertyName("forwarded_from")]
        public string? ForwardedFrom { get; set; }

        private string _from = string.Empty;

        [JsonPropertyName("from")]
        public string From
        {
            get => string.IsNullOrEmpty(_from) ? _actor : _from;
            set => _from = value;
        }

        private string _fromId = string.Empty;

        [JsonPropertyName("from_id")]
        public string FromId
        {
            get => string.IsNullOrEmpty(_fromId) ? _actorId : _fromId;
            set => _fromId = value;
        }

        [JsonPropertyName("height")]
        public int? Height { get; set; }

        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("location_information")]
        public LocationInformation LocationInformation { get; set; }

        [JsonPropertyName("media_type")]
        public string? MediaType { get; set; }

        [JsonPropertyName("members")]
        public IEnumerable<string>? Members { get; set; }

        [JsonPropertyName("message_id")]
        public long? MessageId { get; set; }

        [JsonPropertyName("mime_type")]
        public string? MimeType { get; set; }

        [JsonPropertyName("photo")]
        public string? Photo { get; set; }

        [JsonPropertyName("reply_to_message_id")]
        public long? ReplyToMessageId { get; set; }

        [JsonPropertyName("sticker_emoji")]
        public string? StickerEmoji { get; set; }

        [JsonInclude]
        [JsonPropertyName("text")]
        [JsonConverter(typeof(TextConverter))]
        private Text? _text = default;

        [JsonIgnore]
        public string Text
        {
            get => _text?.FullText ?? string.Empty;

            set
            {
                if (_text is not null)
                {
                    _text.FullText = value;
                }
                else
                {
                    _text = new Text() { FullText = value };
                }
            }
        }

        [JsonPropertyName("text_entities")]
        public IEnumerable<TextEntity>? TextEntities { get; set; }

        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("type")]
        public MessageType? Type { get; set; }

        [JsonPropertyName("via_bot")]
        public string? ViaBot { get; set; }

        [JsonPropertyName("width")]
        public int? Width { get; set; }

        public Message() { }
    }
}
