using System;
using System.Linq;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;
using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Domain entity representing a Question. Questions belong to a user (referenced by UserId),
    /// have a Title, Points, an optional Answer and may be a child of another Question.
    /// This class models behavior and enforces invariants rather than exposing setters.
    /// </summary>
    public class Question : Entity
    {
        #region Properties

        public Title Title { get; private set; } = null!;

        // Points must be zero or positive
        public int Points { get; private set; }

        public ActiveStatus ActiveStatus { get; private set; }
        public Answer? Answer { get; private set; }

        public Question? ParentQuestion { get; private set; }

        private readonly List<Tag> _tags = new();
        public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

        private readonly List<Subject> _subjects = new();
        public IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();

        // Reference to owning user. Keep Id as the canonical reference for persistence and lightweight checks.
        public Guid UserId { get; private set; }

        #endregion


        #region Constructors

        protected Question() { }

        private Question(Title title, int points, ActiveStatus activeStatus, Guid userId, Answer? answer = null, IEnumerable<Tag>? tags = null)
        {
            ValidatePoints(points);

            // Title is a value object; ensure basic creation (no uniqueness checked here)
            Title = Title.Create(title, Array.Empty<string>());
            Points = points;
            ActiveStatus = activeStatus;
            UserId = userId;
            Answer = answer;

            if (tags is not null)
                _tags = tags.ToList();
        }

        #endregion


        #region Factory

        // Create without answer/tags
        public static Question Create(Title title, int points, ActiveStatus activeStatus, Guid userId)
            => new Question(title, points, activeStatus, userId, null, null);

        // Create with tags
        public static Question Create(Title title, int points, ActiveStatus activeStatus, Guid userId, IEnumerable<Tag> tags)
            => new Question(title, points, activeStatus, userId, null, tags);

        // Create with answer (and optional tags)
        public static Question CreateWithAnswer(Title title, int points, ActiveStatus activeStatus, Guid userId, Answer answer, IEnumerable<Tag>? tags = null)
            => new Question(title, points, activeStatus, userId, answer, tags);

        #endregion


        #region Behavior / Relational Methods

        public void UpdateTitle(Title title)
        {
            if (title is null) throw new ErrorException("Title cannot be null", "QUESTION_INVALID");
            Title = Title.Create(title, Array.Empty<string>());
        }

        public void UpdatePoints(int points)
        {
            ValidatePoints(points);
            Points = points;
        }

        public void UpdateActiveStatus(ActiveStatus activeStatus)
        {
            ActiveStatus = activeStatus;
        }

        public void SetAnswer(Answer answer)
        {
            if (answer is null) throw new ErrorException("Answer cannot be null", "QUESTION_INVALID");
            if (Answer is not null) throw new ErrorException("Answer already set. Replace if necessary.", "QUESTION_ANSWER_EXISTS");
            Answer = answer;
        }

        public void ReplaceAnswer(Answer answer)
        {
            if (answer is null) throw new ErrorException("Answer cannot be null", "QUESTION_INVALID");
            Answer = answer;
        }

        public void ClearAnswer()
        {
            Answer = null;
        }

        public void SetParentQuestion(Question? parent)
        {
            if (parent is null)
            {
                ParentQuestion = null;
                return;
            }

            if (ReferenceEquals(parent, this))
                throw new ErrorException("Question cannot be its own parent", "QUESTION_INVALID");

            // Prevent cycles: walk up the chain
            var current = parent;
            while (current is not null)
            {
                if (ReferenceEquals(current, this))
                    throw new ErrorException("Setting this parent would create a cycle", "QUESTION_INVALID");
                current = current.ParentQuestion;
            }

            ParentQuestion = parent;
        }

        public void AddTag(Tag tag)
        {
            if (tag is null) throw new ErrorException("Tag cannot be null", "QUESTION_INVALID");
            if (_tags.Any(t => t.Equals(tag))) return; // idempotent
            _tags.Add(tag);
        }

        public void RemoveTag(Tag tag)
        {
            if (tag is null) return;
            _tags.RemoveAll(t => t.Equals(tag));
        }

        public void AddSubject(Subject subject)
        {
            if (subject is null) throw new ErrorException("Subject cannot be null", "QUESTION_INVALID");
            if (_subjects.Any(s => s.Equals(subject))) return;
            _subjects.Add(subject);
        }

        public void RemoveSubject(Subject subject)
        {
            if (subject is null) return;
            _subjects.RemoveAll(s => s.Equals(subject));
        }

       /* public void AssignToUser(Guid userId)
        {
            if (userId == Guid.Empty) throw new ErrorException("UserId must be provided", "QUESTION_INVALID");
            UserId = userId;
        }*/

        #endregion


        #region Validation

        private static void ValidatePoints(int points)
        {
            if (points < 0) throw new ErrorException("Points must be zero or positive", "QUESTION_INVALID");
        }

        #endregion
    }
}
