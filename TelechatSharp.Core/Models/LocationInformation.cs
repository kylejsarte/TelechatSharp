using System.Text.Json.Serialization;

namespace TelechatSharp.Core.Models
{
    public class LocationInformation
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
