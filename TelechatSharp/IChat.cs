using TelechatSharp.Core.Models;

namespace TelechatSharp.Core
{
    public interface IChat
    {
        public string? Name { get; set; }

        public string? Type { get; set; }

        public long? Id { get; set; }

        public IEnumerable<Message>? Messages { get; set; }
    }
}
