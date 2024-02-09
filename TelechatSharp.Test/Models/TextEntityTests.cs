using System.Text.Json;
using TelechatSharp.Core.Models;

namespace TelechatSharp.Test.Models
{
    public class TextEntityTests
    {
        [Fact]
        public void TextEntityConstructor_WithPlainText_SetsTypeAndText()
        {
            var plainText = "Hello, friend.";
            var token = JsonDocument.Parse($"\"{plainText}\"").RootElement;

            var textEntity = new TextEntity(token);

            Assert.Equal("plain", textEntity.Type);
            Assert.Equal(plainText, textEntity.Text);
        }

        [Fact]
        public void TextEntityConstructor_WithJsonObject_SetsTypeAndText()
        {
            var json = "{\"type\":\"TEST_TYPE\",\"text\":\"TEST_TEXT\"}";
            var token = JsonDocument.Parse(json).RootElement;

            var textEntity = new TextEntity(token);

            Assert.Equal("TEST_TYPE", textEntity.Type);
            Assert.Equal("TEST_TEXT", textEntity.Text);
        }
    }
}
