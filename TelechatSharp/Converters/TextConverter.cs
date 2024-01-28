using System.Text.Json;
using System.Text.Json.Serialization;
using TelechatSharp.Core.Models;

namespace TelechatSharp.Core.Converters
{
    internal class TextConverter : JsonConverter<Text>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(Text).IsAssignableFrom(typeToConvert);
        }

        public override Text Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var document = JsonDocument.ParseValue(ref reader);

            return new Text(document.RootElement);
        }

        public override void Write(Utf8JsonWriter writer, Text value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}
