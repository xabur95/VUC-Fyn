namespace Semesterprojekt1PBA.Frontend.Models;

public class Question
{
    public Guid Id { get; set; }

    public byte[] RowVersion { get; set; } = [];

    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public int Points { get; set; }

    public string ActiveStatus { get; set; } = string.Empty;

    public Guid CreatedByUserId { get; set; }

    public Guid? ParentQuestionId { get; set; }

    public List<Tag> Tags { get; set; } = [];

    public List<Subject> Subjects { get; set; } = [];

    public Answer? Answer { get; set; }
}