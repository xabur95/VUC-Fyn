using System.Text.Json.Serialization;

namespace Semesterprojekt1PBA.Frontend.Models
{
    public class Tag
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
    }
}
