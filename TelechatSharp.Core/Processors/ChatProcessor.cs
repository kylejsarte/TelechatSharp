using System.Text.Json;
using TelechatSharp.Core;

namespace TelechatSharp.Core.Processors
{
    internal static class ChatProcessor
    {
        internal static Chat DeserializeChatFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"{filePath} does not exist.");
                }

                string fileContent = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<Chat>(fileContent, options) ?? throw new InvalidOperationException("JSON deserialization returned null.");
            }
            catch (JsonException jsonException)
            {
                throw new InvalidOperationException("JSON deserialization failed.", jsonException);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Error reading file.", exception);
            }
        }

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
