using TelechatSharp.Core;
using TelechatSharp.Core.Deserializers;

namespace TelechatSharp.Test.Deserializers
{
    public class ChatDeserializerTests
    {
        private string EmptyChatJsonPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "empty_chat.json");
            
        [Fact]
        public void DeserializeChatFromFile_ThrowsArgumentException_WhenFilePathIsNull()
        {
            var expectedErrorMessage = "File path cannot be null or empty. (Parameter 'filePath')";

            var exception = Assert.Throws<ArgumentException>(() => ChatDeserializer.DeserializeChatFromFile(null));

            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void DeserializeChatFromFile_ThrowsArgumentException_WhenFilePathIsEmpty()
        {
            var expectedErrorMessage = "File path cannot be null or empty. (Parameter 'filePath')";

            var exception = Assert.Throws<ArgumentException>(() => ChatDeserializer.DeserializeChatFromFile(string.Empty));

            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void DeserializeChatFromFile_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
            var filePath = "does_not_exist.json";
            
            var expectedErrorMessage = $"{filePath} does not exist.";

            var exception = Assert.Throws<FileNotFoundException>(() => ChatDeserializer.DeserializeChatFromFile(filePath));

            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void DeserializeChatFromStream_ThrowsInvalidOperationException_WhenFileContentIsEmpty()
        {
            using var memoryStream = new MemoryStream();
            using var streamReader = new StreamReader(memoryStream);

            var expectedErrorMessage = "File content cannot be empty.";

            var exception = Assert.Throws<InvalidOperationException>(() => ChatDeserializer.DeserializeChatFromStream(streamReader));

            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void DeserializeChatFromStream_ThrowsInvalidOperationException_WhenFileContentContainsNull()
        {
            using var memoryStream = new MemoryStream();

            using var streamWriter = new StreamWriter(memoryStream);
                
            streamWriter.WriteLine("null");

            streamWriter.Flush();

            memoryStream.Seek(0, SeekOrigin.Begin);

            using var streamReader = new StreamReader(memoryStream);

            var expectedErrorMessage = "JSON deserialization returned null.";

            var exception = Assert.Throws<InvalidOperationException>(() => ChatDeserializer.DeserializeChatFromStream(streamReader));

            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Fact]
        public void DeserializeChatFromFile_ReturnsChatObject_WhenJsonDeserializationSucceeds()
        {
            var chat = ChatDeserializer.DeserializeChatFromFile(EmptyChatJsonPath);

            Assert.NotNull(chat);
            Assert.IsType<Chat>(chat);
        }

        [Fact]
        public void DeserializeChatFromStream_ReturnsChatObject_WhenJsonDeserializationSucceeds()
        {
            using var streamReader = File.OpenText(EmptyChatJsonPath);

            var chat = ChatDeserializer.DeserializeChatFromStream(streamReader);

            Assert.NotNull(chat);
            Assert.IsType<Chat>(chat);
        }
    }
}
