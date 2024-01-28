using System.Text.Json.Serialization;
using TelechatSharp.Core.Models;
using TelechatSharp.Core.Processors;

namespace TelechatSharp.Core
{
    public class Chat : IChat
    {
        public string? Name { get; set; }

        public string? Type { get; set; }

        public long? Id { get; set; }

        [JsonRequired]
        public IEnumerable<Message> Messages { get; set; } = default!;

        public Chat(string filePath)
        {
            var chat = ChatProcessor.DeserializeChatFromFile(filePath);

            Name = chat.Name;
            Type = chat.Type;
            Id = chat.Id;
            Messages = chat.Messages;
        }

        [JsonConstructor]
        private Chat() { }
    }
}