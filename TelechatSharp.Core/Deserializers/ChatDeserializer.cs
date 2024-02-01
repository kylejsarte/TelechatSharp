﻿using System.Text.Json;

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

            try
            {
                string fileContent = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<Chat>(fileContent, options) ?? throw new InvalidOperationException("JSON deserialization returned null.");
            }
            catch (JsonException jsonException)
            {
                throw new InvalidOperationException("JSON deserialization failed.", jsonException);
            }
        }

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
