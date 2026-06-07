namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// A single score given to one Student on one Question on an EvaluationSheet.
    /// Used for both teacher scoring and student self-assessment.
    /// </summary>
    public class QuestionScore : Entity
    {
        public Guid StudentId { get; private set; }
        public Guid QuestionId { get; private set; }
        public int Points { get; private set; }
        public Guid ScoredByUserId { get; private set; }

        protected QuestionScore() { }

        private QuestionScore(Guid studentId, Guid questionId, int points, Guid scoredByUserId)
        {
            StudentId = studentId;
            QuestionId = questionId;
            Points = points;
            ScoredByUserId = scoredByUserId;
        }

        public static QuestionScore Create(Guid studentId, Guid questionId, int points, Guid scoredByUserId)
            => new(studentId, questionId, points, scoredByUserId);

        public void UpdatePoints(int points, Guid scoredByUserId)
        {
            Points = points;
            ScoredByUserId = scoredByUserId;
        }
    }
}