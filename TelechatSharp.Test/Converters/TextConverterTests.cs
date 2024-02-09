using Moq;
using System.Text;
using System.Text.Json;
using TelechatSharp.Core.Converters;
using TelechatSharp.Core.Models;
using TelechatSharp.Test.TestData;

namespace TelechatSharp.Test.Converters
{
    public class TextConverterTests
    {
        [Theory]
        [InlineData(typeof(Text), true)]
        [InlineData(typeof(object), false)]
        public void CanConvert_ReturnsExpectedBool(Type typeToConvert, bool expected)
        {
            bool result = new TextConverter().CanConvert(typeToConvert);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Read_ReturnsTextObject_FromValidJsonString()
        {
            var jsonString = "\"TEST\"";
            var bytes = Encoding.UTF8.GetBytes(jsonString);
            var reader = new Utf8JsonReader(bytes);

            reader.Read();

            var result = new TextConverter().Read(ref reader, typeof(Text), It.IsAny<JsonSerializerOptions>());

            Assert.NotNull(result);

            Assert.IsType<Text>(result);

            Assert.Equal("TEST", result.FullText);
        }

        [Theory]
        [MemberData(nameof(JsonObjects.JsonArrays), MemberType = typeof(JsonObjects))]

        public void Read_ReturnsTextObject_FromValidJsonArray(object[] jsonArray, string expectedFullText)
        {
            string jsonString = JsonSerializer.Serialize(jsonArray);

            var bytes = Encoding.UTF8.GetBytes(jsonString);
            var reader = new Utf8JsonReader(bytes);

            reader.Read();

            var result = new TextConverter().Read(ref reader, typeof(Text), It.IsAny<JsonSerializerOptions>());

            Assert.NotNull(result);

            Assert.IsType<Text>(result);

            Assert.Equal(expectedFullText, result.FullText);
        }

        [Fact]
        public void Write_ThrowsNotSupportedException()
        {
            Assert.Throws<NotSupportedException>(() => new TextConverter().Write(
                It.IsAny<Utf8JsonWriter>(),
                It.IsAny<Text>(),
                It.IsAny<JsonSerializerOptions>()));
        }
    }
}
