using TelechatSharp.Core;

namespace TelechatSharp.Test
{
    public class ChatTests
    {
        private string TestChatJsonPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "test_chat.json");

        [Fact]
        public void ChatConstructor_InitializesChat_WithFilePath()
        {
            var chat = new Chat(TestChatJsonPath);

            Assert.NotNull(chat);

            Assert.Equal("TEST_NAME", chat.Name);
            Assert.Equal("TEST_TYPE", chat.Type);
            Assert.Equal(0, chat.Id);
            Assert.NotNull(chat.Messages);
        }

        [Fact]
        public void ChatConstructor_InitializesChat_WithStreamReader()
        {
            using var streamReader = File.OpenText(TestChatJsonPath);

            var chat = new Chat(streamReader);

            Assert.NotNull(chat);

            Assert.Equal("TEST_NAME", chat.Name);
            Assert.Equal("TEST_TYPE", chat.Type);
            Assert.Equal(0, chat.Id);
            Assert.NotNull(chat.Messages);
        }
    }
}