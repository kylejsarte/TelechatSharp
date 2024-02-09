using System.Text.Json;

namespace TelechatSharp.Core.Deserializers
{
    internal static class ChatDeserializer
    {
        internal static Chat DeserializeChatFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{filePath} does not exist.");
            }

            var streamReader = File.OpenText(filePath);

            return DeserializeChatFromStream(streamReader);
        }

        internal static Chat DeserializeChatFromStream(StreamReader streamReader)
        {
            string fileContent = streamReader.ReadToEnd();

            if (string.IsNullOrEmpty(fileContent))
            {
                throw new InvalidOperationException("File content cannot be empty.");
            }

            return JsonSerializer.Deserialize<Chat>(fileContent, options) ?? throw new InvalidOperationException("JSON deserialization returned null.");
        }

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }
}