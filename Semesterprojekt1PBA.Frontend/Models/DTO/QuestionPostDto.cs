namespace Semesterprojekt1PBA.Frontend.Models.DTO
{
    public class QuestionPostDto
    {
        public Guid CreatedByUserId { get; set; }
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public int Points { get; set; }
        public int ActiveStatus { get; set; }
        public Guid? ParentQuestionId { get; set; }
        public List<Guid> TagIds { get; set; } = new();
        public List<Guid> SubjectIds { get; set; } = new();
    }
}
