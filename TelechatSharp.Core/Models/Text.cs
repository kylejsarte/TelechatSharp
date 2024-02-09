using System.Text;
using System.Text.Json;

namespace TelechatSharp.Core.Models
{
    public class Text
    {
        public string FullText { get; set; } = string.Empty;

        public Text() { }

        internal Text(JsonElement token)
        {
            FullText = GetFullText(token);
        }

        private string GetFullText(JsonElement token)
        {
            StringBuilder fullText = new StringBuilder();

            fullText.AppendJoin(string.Empty, GetTextElements(token).Select(textNode => textNode.Text));

            return fullText.ToString();
        }

        private IEnumerable<TextEntity> GetTextElements(JsonElement token)
        {
            if (token.ValueKind == JsonValueKind.String)
            {
                yield return new TextEntity(token);
            }

            if (token.ValueKind == JsonValueKind.Array)
            {
                foreach (var textNode in token.EnumerateArray())
                {
                    yield return new TextEntity(textNode);
                }
            }
        }

        public override string? ToString() => FullText;
    }
}
