namespace Semesterprojekt1PBA.Frontend.Models.Filters
{
    public class QuestionFilter
    {
        public string? Subject { get; set; }
        public string? Tag { get; set; }
        public string? Text { get; set; }
        public int? MinPoints { get; set; }
        public int? ActiveStatus { get; set; }
        public bool ShowMyQuestionsOnly { get; set; }
    }
}
