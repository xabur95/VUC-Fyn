namespace Semesterprojekt1PBA.Frontend.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        Guid createdByUserId { get; set; }
    }
}
