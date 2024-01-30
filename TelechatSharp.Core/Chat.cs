using System.Text.Json.Serialization;
using TelechatSharp.Core.Deserializers;
using TelechatSharp.Core.Models;

namespace TelechatSharp.Core
{
    public class Chat : IChat
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]  
        public string? Type { get; set; }

        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("messages")]
        public IEnumerable<Message>? Messages { get; set; }

        public Chat(string filePath)
        {
            var chat = ChatDeserializer.DeserializeChatFromFile(filePath);

            Name = chat.Name;
            Type = chat.Type;
            Id = chat.Id;
            Messages = chat.Messages;
        }

        [JsonConstructor]
        private Chat() { }
    }
}