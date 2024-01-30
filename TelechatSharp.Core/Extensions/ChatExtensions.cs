using TelechatSharp.Core.Models;

namespace TelechatSharp.Core.Extensions
{
    public static class ChatExtensions
    {
        public static DateTime? GetDateCreated(this Chat chat)
        {
            _ = chat ?? throw new ArgumentNullException(nameof(chat));

            // When the exported JSON is a complete group chat history containing the oldest message, 
            // the "create_group" action will be in the first messages element.
            var firstMessage = chat.Messages?.FirstOrDefault();

            return string.Equals(firstMessage?.Action, "create_group") ? firstMessage?.Date : null;
        }

        public static IEnumerable<Member> GetMembers(this Chat chat)
        {
            _ = chat ?? throw new ArgumentNullException(nameof(chat));

            if (chat.Messages is null)
            {
                return Enumerable.Empty<Member>();
            }

            return chat.Messages
                .Select(message => new Member
                {
                    Id = message.FromId ?? message.ActorId,
                    Name = message.From ?? message.Actor
                })
                .Where(user => !string.IsNullOrEmpty(user.Id) || !string.IsNullOrEmpty(user.Name))
                .Distinct();
        }

        public static IEnumerable<Member> GetOriginalMembers(this Chat chat)
        {
            _ = chat ?? throw new ArgumentNullException(nameof(chat));

            if (chat.Messages is null || !chat.Messages.Any() || chat.Messages.FirstOrDefault()?.Members is null)
            {
                return Enumerable.Empty<Member>();
            }

            var originalMembers = chat.Messages.FirstOrDefault()?.Members;

            return originalMembers.Select(member => new Member
            {
                Name = member
            });
        }
    }
}