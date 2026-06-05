using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Frontend.Models
{
    public class QuestionFilter
    {
        public string? Title { get; set; }
        public string? Text { get; set;  }
        public int Points { get; set; }
        public ActiveStatus? ActiveStatus { get; set; }
        public string? ParentQuestion { get; set; }
        public string? Answer { get; set; }
        public int? CreatedByUserId { get; set; }
    }
}
