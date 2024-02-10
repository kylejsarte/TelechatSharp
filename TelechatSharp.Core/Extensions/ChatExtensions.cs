using TelechatSharp.Core.Models;

namespace TelechatSharp.Core.Extensions
{
    public static class ChatExtensions
    {
        public static DateTime? GetDateCreated(this IChat chat)
        {
            _ = chat ?? throw new ArgumentNullException(nameof(chat));

            var createGroupMessage = chat.Messages?.Where(message => message.Action == "create_group").FirstOrDefault();

            return createGroupMessage?.Date;
        }

        public static IEnumerable<Member> GetMembers(this IChat chat)
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

        public static IEnumerable<Member> GetOriginalMembers(this IChat chat)
        {
            _ = chat ?? throw new ArgumentNullException(nameof(chat));

            var createGroupMessage = chat.Messages?.Where(message => message.Action == "create_group").FirstOrDefault();

            var originalMembers = createGroupMessage?.Members;

            return originalMembers?.Select(member => new Member
            {
                Name = member
            }) ?? Enumerable.Empty<Member>();
        }
    }
}