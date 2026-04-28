using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Answer is part of the <see cref="Question"/> aggregate.
    /// Constructible only from inside the domain assembly via the aggregate root.
    /// </summary>
    public class Answer : Entity
    {
        public Title Title { get; private set; }
        public string Text { get; private set; }

        public Guid QuestionId { get; private set; }
        public Guid CreatedByUserId { get; private set; }

        // EF Core binds to this constructor by parameter-name → property-name matching.
        internal Answer(Guid questionId, Title title, string text, Guid createdByUserId)
        {
            if (questionId == Guid.Empty)
                throw new ErrorException("QuestionId is required for an answer.", "ANSWER_INVALID");
            if (string.IsNullOrWhiteSpace(text))
                throw new ErrorException("Answer text cannot be empty.", "ANSWER_INVALID");
            if (createdByUserId == Guid.Empty)
                throw new ErrorException("Answer requires an author.", "ANSWER_INVALID");

            QuestionId = questionId;
            Title = title;
            Text = text;
            CreatedByUserId = createdByUserId;
        }

        internal void UpdateContent(Title title, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ErrorException("Answer text cannot be empty.", "ANSWER_INVALID");

            Title = title;
            Text = text;
        }
    }
}
