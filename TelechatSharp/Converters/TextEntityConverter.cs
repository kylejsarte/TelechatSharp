using System.Text.Json;
using System.Text.Json.Serialization;
using TelechatSharp.Core.Models;

namespace TelechatSharp.Core.Converters
{
    internal class TextEntityConverter : JsonConverter<IEnumerable<TextEntity>>
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<TextEntity>).IsAssignableFrom(objectType);
        }

        public override IEnumerable<TextEntity> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var document = JsonDocument.ParseValue(ref reader);

            var textEntities = new List<TextEntity>();

            foreach (var element in document.RootElement.EnumerateArray())
            {
                textEntities.Add(new TextEntity(element));
            }

            return textEntities;
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<TextEntity> value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }
    }
}