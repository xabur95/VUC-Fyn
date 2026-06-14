using System.Text.Json.Serialization;

namespace Semesterprojekt1PBA.Frontend.Models.DTO
{
    public class TagGetDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("rowVersion")]
        public byte[] RowVersion { get; set; } = [];

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
