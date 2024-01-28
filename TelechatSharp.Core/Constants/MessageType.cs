using System.Text.Json.Serialization;

namespace TelechatSharp.Core.Constants
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageType
    {
        Message,
        Service
    }
}