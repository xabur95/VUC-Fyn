using System.Text.Json.Serialization;

namespace Semesterprojekt1PBA.Frontend.Models;

public class Question
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("rowVersion")]
    public byte[] RowVersion { get; set; } = [];
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
    [JsonPropertyName("points")]
    public int Points { get; set; }
    [JsonPropertyName("activeStatus")]
    public int ActiveStatus { get; set; }
    [JsonPropertyName("createdByUserId")]
    public Guid CreatedByUserId { get; set; }
    [JsonPropertyName("parentQuestionId")]
    public Guid? ParentQuestionId { get; set; }
    [JsonPropertyName("tags")]
    public List<Tag> Tags { get; set; } = [];
    [JsonPropertyName("subjects")]
    public List<Subject> Subjects { get; set; } = [];
    [JsonPropertyName("answer")]
    public Answer? Answer { get; set; }
}