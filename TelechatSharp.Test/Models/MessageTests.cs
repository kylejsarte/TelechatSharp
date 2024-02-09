using System.Text.Json;
using TelechatSharp.Core.Models;

namespace TelechatSharp.Test.Models
{
    public class MessageTests
    {
        [Theory]
        [InlineData("Elliot Alderson", null, "Elliot Alderson")]
        [InlineData(null, "Darlene Alderson", "Darlene Alderson")]
        public void Actor_From_Gets_ReturnExpectedValue(string actor, string from, string expectedName)
        {
            var message = new Message { Actor = actor, From = from };

            Assert.Equal(expectedName, message.Actor);
            Assert.Equal(expectedName, message.From);
        }

        [Theory]
        [InlineData("Asher Siegel", null, "Asher Siegel")]
        [InlineData(null, "Whitney Siegel", "Whitney Siegel")]
        public void ActorId_FromId_Gets_ReturnExpectedValue(string actorId, string fromId, string expectedId)
        {
            var message = new Message { ActorId = actorId, FromId = fromId };

            Assert.Equal(expectedId, message.ActorId);
            Assert.Equal(expectedId, message.FromId);
        }

        [Fact]
        public void Actor_Set_OverridesBackingField()
        {
            var message = new Message { Actor = "Hershel Layton" };

            message.Actor = "Jean Descole";

            Assert.Equal("Jean Descole", message.Actor);
        }

        [Fact]
        public void ActorId_Set_OverridesBackingField()
        {
            var message = new Message { ActorId = "Mima Kirigoe" };

            message.ActorId = "Rumi";

            Assert.Equal("Rumi", message.ActorId);
        }

        [Fact]
        public void From_Set_OverridesBackingField()
        {
            var message = new Message { From = "Carmen Berzatto" };

            message.From = "Richard Jerimovich";

            Assert.Equal("Richard Jerimovich", message.From);
        }

        [Fact]
        public void FromId_Set_OverridesBackingField()
        {
            var message = new Message { FromId = "Ephraim Winslow" };

            message.FromId = "Thomas Wake";

            Assert.Equal("Thomas Wake", message.FromId);
        }

        [Fact]
        public void Text_Get_WhenTextIsDeserialized_ReturnsText()
        {
            var message = JsonSerializer.Deserialize<Message>("{\"text\":\"TEST\"}");

            Assert.Equal("TEST", message!.Text);
        }

        [Fact]
        public void Text_Get_WhenTextIsNull_ReturnsEmptyString()
        {
            var message = JsonSerializer.Deserialize<Message>("{}");

            Assert.Equal(string.Empty, message!.Text);
        }

        [Fact]
        public void Text_Set_WhenTextIsNotNull_OverridesBackingField()
        {
            var message = JsonSerializer.Deserialize<Message>("{\"text\":\"TEST\"}");

            Assert.Equal("TEST", message!.Text);

            var expectedText = "NEW";

            message.Text = expectedText;

            Assert.Equal(expectedText, message.Text);
        }

        [Fact]
        public void Text_Set_WhenTextIsNull_CreatesNewTextWithSpecifiedFullText()
        {
            var message = JsonSerializer.Deserialize<Message>("{}");

            Assert.Equal(string.Empty, message!.Text);

            var expectedText = "TEST";

            message.Text = expectedText;

            Assert.Equal(expectedText, message.Text);
        }
    }
}
