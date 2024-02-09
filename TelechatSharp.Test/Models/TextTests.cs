using System.Text.Json;
using TelechatSharp.Core.Models;
using TelechatSharp.Test.TestData;

namespace TelechatSharp.Test.Models
{
    public class TextTests
    {
        [Fact]
        public void TextConstructor_WithNullJsonElement_InitializesEmptyFullText()
        {
            var token = JsonDocument.Parse("null").RootElement;

            var text = new Text(token);

            Assert.Equal(string.Empty, text.FullText);
        }

        [Fact]
        public void TextConstructor_WithPlainText_InitializesFullText()
        {
            var token = JsonDocument.Parse("\"Hello, friend.\"").RootElement;

            var text = new Text(token);

            Assert.Equal("Hello, friend.", text.FullText);
        }

        [Theory]
        [MemberData(nameof(JsonObjects.JsonArrays), MemberType = typeof(JsonObjects))]
        public void TextConstructor_WithJsonArray_InitializesFullText(object[] jsonArray, string expectedFullText)
        {
            string jsonString = JsonSerializer.Serialize(jsonArray);

            var token = JsonDocument.Parse(jsonString).RootElement;

            var text = new Text(token);

            Assert.Equal(expectedFullText, text.FullText);
        }

        [Fact]
        public void ToString_ReturnsFullText()
        {
            var text = new Text { FullText = "Hello, friend." };

            var result = text.ToString();

            Assert.Equal("Hello, friend.", result);
        }
    }
}
