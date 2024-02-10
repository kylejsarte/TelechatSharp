using Moq;
using TelechatSharp.Core;
using TelechatSharp.Core.Extensions;
using TelechatSharp.Core.Models;

namespace TelechatSharp.Test.Extensions
{
    public class ChatExtensionsTests
    {
        [Fact]
        public void GetDateCreated_ThrowsArgumentNullException_WhenChatIsNull()
        {
            Chat chat = null!;

            Assert.Throws<ArgumentNullException>(() => chat!.GetDateCreated());
        }

        [Fact]
        public void GetDateCreated_ReturnsNull_WhenMessagesIsNull()
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns((IEnumerable<Message>)null!);

            var result = mockChat.Object.GetDateCreated();

            Assert.Null(result);
        }

        [Fact]
        public void GetDateCreated_ReturnsNull_WhenMessagesDoesNotContainCreateGroupAction()
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns(new List<Message> { new Message { } });

            var result = mockChat.Object.GetDateCreated();

            Assert.Null(result);
        }

        [Fact]
        public void GetDateCreated_ReturnsDateCreated_WhenItExists()
        {
            var expectedDate = new DateTime(1995, 1, 25);

            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns(new List<Message>
            {
                new Message
                {
                    Action = "create_group",
                    Date = expectedDate
                }
            });

            var result = mockChat.Object.GetDateCreated();

            Assert.NotNull(result);

            Assert.Equal(expectedDate, result);
        }

        [Fact]
        public void GetMembers_ThrowsArgumentNullException_WhenChatIsNull()
        {
            Chat chat = null;

            Assert.Throws<ArgumentNullException>(() => chat!.GetMembers());
        }

        [Fact]
        public void GetMembers_ReturnsEmptyCollection_WhenMessagesIsNull()
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns((IEnumerable<Message>)null!);

            var result = mockChat.Object.GetMembers();

            Assert.Empty(result);
        }

        [Fact]
        public void GetMembers_ReturnsEmptyCollection_WhenMessagesIsEmpty()
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns(new List<Message>());

            var result = mockChat.Object.GetMembers();

            Assert.Empty(result);
        }

        [Theory]
        [MemberData(nameof(MemberAndMessageData))]
        public void GetMembers_ReturnsMembers_WhenMessagesContainsMembers(List<Member> expectedMembers, List<Message> messageList)
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns(messageList);

            var result = mockChat.Object.GetMembers();

            Assert.NotEmpty(result);

            Assert.Equal(expectedMembers, result);
        }

        [Fact]
        public void GetOriginalMembers_ThrowsArgumentNullException_WhenChatIsNull()
        {
            Chat chat = null;

            Assert.Throws<ArgumentNullException>(() => chat!.GetOriginalMembers());
        }

        [Fact]
        public void GetOriginalMembers_ReturnsEmptyCollection_WhenMessagesIsNull()
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns((IEnumerable<Message>)null!);

            var result = mockChat.Object.GetOriginalMembers();

            Assert.NotNull(result);

            Assert.Empty(result);
        }

        [Fact]
        public void GetOriginalMembers_ReturnsEmptyCollection_WhenMessagesIsEmpty()
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns(new List<Message>());

            var result = mockChat.Object.GetOriginalMembers();

            Assert.NotNull(result);

            Assert.Empty(result);
        }

        [Fact]
        public void GetOriginalMembers_ReturnsEmptyCollection_WhenCreateGroupActionDoesNotExist()
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns(new List<Message> { new Message() });

            var result = mockChat.Object.GetOriginalMembers();

            Assert.Empty(result);
        }

        [Fact]
        public void GetOriginalMembers_ReturnsOriginalMembers_WhenCreateGroupActionExists()
        {
            var expectedMembers = new List<Member>
            {
                new Member { Name = "L" },
                new Member { Name = "Near" }
            };

            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns(new List<Message>
            {
                new Message
                {
                    Action = "create_group",
                    Members = new List<string> { "L", "Near" }
                }
            });

            var result = mockChat.Object.GetOriginalMembers();

            Assert.NotNull(result);

            Assert.NotEmpty(result);

            Assert.Equal(expectedMembers, result);
        }

        [Fact]
        public void GetOriginalMembers_ReturnsOriginalMembers_WhenFirstMessageMembersIsEmpty()
        {
            var mockChat = new Mock<IChat>();

            mockChat.SetupGet(chat => chat.Messages).Returns(new List<Message>
            {
                new Message
                {
                    Action = "create_group",
                    Members = new List<string>()
                }
            });

            var result = mockChat.Object.GetOriginalMembers();

            Assert.NotNull(result);

            Assert.Empty(result);
        }

        public static IEnumerable<object[]> MemberAndMessageData => new[]
        {
            new object[]
            {
                new List<Member>
                {
                    new Member { Id = "1", Name = "L" },
                    new Member { Id = "2", Name = "Near" }
                },
                new List<Message>
                {
                    new Message { FromId = "1", From = "L" },
                    new Message { FromId = "2", From = "Near" }
                },
            },
            new object[]
            {
                new List<Member>
                {
                    new Member { Id = "1", Name = "René Magritte" },
                    new Member { Id = "2", Name = "Julio Cortázar" }
                },
                new List<Message>
                {
                    new Message { ActorId = "1", Actor = "René Magritte" },
                    new Message { ActorId = "2", Actor = "Julio Cortázar" }
                }
            }
        };
    }
}
