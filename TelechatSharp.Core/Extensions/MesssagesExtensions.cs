using TelechatSharp.Core.Constants;
using TelechatSharp.Core.Models;

namespace TelechatSharp.Core.Extensions
{
    public static class MesssagesExtensions
    {
        /// <summary>
        /// Returns a collection of all <see cref="MessageType.Message"/> type messages in the chat.
        /// </summary>
        public static IEnumerable<Message>? GetMessageTypeMessages(this IEnumerable<Message> messages)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));

            return messages.Where(m => m.Type == MessageType.Message);
        }

        /// <summary>
        /// Returns a collection of all <see cref="MessageType.Service"/> type messages in the chat.
        /// </summary>
        public static IEnumerable<Message>? GetServiceTypeMessages(this IEnumerable<Message> messages)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));

            return messages.Where(m => m.Type == MessageType.Service);
        }

        public static IEnumerable<Message>? GetPhotoMessages(this IEnumerable<Message> messages)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));

            return messages.Where(message => !string.IsNullOrEmpty(message.Photo));
        }

        public static IEnumerable<Message>? GetVideoFileMessages(this IEnumerable<Message> messages)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));

            return messages.Where(message => string.Equals(message.MediaType, "video_file"));
        }

        public static IEnumerable<Message>? GetVideoMessageMessages(this IEnumerable<Message> messages)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));

            return messages.Where(message => string.Equals(message.MediaType, "video_message"));
        }

        /// <summary>
        /// Returns a collection of all unique text entity in the chat.
        /// </summary>
        public static IEnumerable<string>? GetTextEntityTypes(this IEnumerable<Message> messages)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));

            return messages
                .Where(message => message.TextEntities is not null)
                .SelectMany(message => message.TextEntities!)
                .Where(textEntity => !string.IsNullOrEmpty(textEntity.Type))
                .Select(textEntity => textEntity.Type!)
                .Distinct();
        }

        /// <summary>
        /// Returns a collection of all unique media types in the chat.
        /// </summary>
        public static IEnumerable<string>? GetMediaTypes(this IEnumerable<Message> messages)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));

            return messages
                .Where(message => !string.IsNullOrEmpty(message.MediaType))
                .Select(message => message.MediaType!)
                .Distinct();
        }

        /// <summary>
        /// Returns a collection of "text" property values for a specific text entity type. For example, get all emails or phone numbers sent in the chat.
        /// </summary>
        public static IEnumerable<string?>? GetAllTextsOfTextEntityType(this IEnumerable<Message> messages, string type)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));
            _ = type ?? throw new ArgumentNullException(nameof(type));

            return messages
                .Where(message => message.TextEntities is not null)
                .SelectMany(message => message.TextEntities!)
                .Where(textEntity => textEntity.Type == type)
                .Select(textEntity => textEntity.Text);
        }

        /// <summary>
        /// Returns a collection of messages which contain a specific text entity type. For example, get all messages that contain an email or phone number.
        /// </summary>
        public static IEnumerable<Message>? GetAllMessagesWithTextEntityType(this IEnumerable<Message> messages, string type)
        {
            _ = messages ?? throw new ArgumentNullException(nameof(messages));
            _ = type ?? throw new ArgumentNullException(nameof(type));

            return messages
                .Where(message => message.TextEntities is not null && message.TextEntities.Any(textEntity => textEntity.Type == type));
        }
    }
}
