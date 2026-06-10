using Semesterprojekt1PBA.Domain.Helpers;

namespace Semesterprojekt1PBA.Domain.Entities
{
    /// <summary>
    /// Represents an evaluation sheet for an entire Class.
    /// Tracks points awarded to each Student on each Question,
    /// both by the teacher and by the student (self-assessment).
    ///
    /// Questions belong to their own aggregate; this sheet only references
    /// them by Id. The Application layer typically loads questions from an
    /// AssignmentSheet and hands them to <see cref="Create"/>.
    /// </summary>
    public class EvaluationSheet : Entity
    {
        #region Properties

        public Class Class { get; protected set; } = null!;

        // Snapshot of which questions this sheet covers (by Id only).
        private readonly List<Guid> _questionIds = [];
        public IReadOnlyCollection<Guid> QuestionIds => _questionIds;

        // Teacher's scoring of each student on each question.
        private readonly List<QuestionScore> _teacherScores = [];
        public IReadOnlyCollection<QuestionScore> TeacherScores => _teacherScores;

        // Student's self-scoring on each question.
        private readonly List<QuestionScore> _studentScores = [];
        public IReadOnlyCollection<QuestionScore> StudentScores => _studentScores;

        #endregion

        #region Constructors

        protected EvaluationSheet() { } // EF

        private EvaluationSheet(Class @class, IEnumerable<Guid> questionIds)
        {
            Class = @class;
            _questionIds = questionIds.Distinct().ToList();
        }

        #endregion

        #region Factory

        /// <summary>
        /// Create an EvaluationSheet for a Class from a set of Questions.
        /// Only the Question Ids are stored; Questions remain their own aggregate.
        /// </summary>
        public static EvaluationSheet Create(Class @class, IEnumerable<Question> questions)
        {
            if (@class is null)
                throw new ErrorException("Class cannot be null.", "EVALUATIONSHEET_INVALID");
            if (questions is null)
                throw new ErrorException("Questions cannot be null.", "EVALUATIONSHEET_INVALID");

            var ids = questions.Select(q => q.Id).ToList();
            if (ids.Count == 0)
                throw new ErrorException(
                    "EvaluationSheet must contain at least one question.",
                    "EVALUATIONSHEET_EMPTY");

            return new EvaluationSheet(@class, ids);
        }

        #endregion

        #region Scoring – Teacher

        public void SetTeacherScore(Teacher teacher, Student student, Guid questionId, int points)
        {
            if (teacher is null)
                throw new ErrorException("Teacher cannot be null.", "EVALUATIONSHEET_INVALID");

            EnsureBelongsToSheet(student, questionId);

            var existing = _teacherScores
                .FirstOrDefault(s => s.StudentId == student.Id && s.QuestionId == questionId);

            if (existing is null)
                _teacherScores.Add(QuestionScore.Create(student.Id, questionId, points, teacher.Id));
            else
                existing.UpdatePoints(points, teacher.Id);
        }

        #endregion

        #region Scoring – Student (self-assessment)

        public void SetStudentScore(Student student, Guid questionId, int points)
        {
            EnsureBelongsToSheet(student, questionId);

            var existing = _studentScores
                .FirstOrDefault(s => s.StudentId == student.Id && s.QuestionId == questionId);

            if (existing is null)
                _studentScores.Add(QuestionScore.Create(student.Id, questionId, points, student.Id));
            else
                existing.UpdatePoints(points, student.Id);
        }

        #endregion

        #region Invariants

        private void EnsureBelongsToSheet(Student student, Guid questionId)
        {
            if (student is null)
                throw new ErrorException("Student cannot be null.", "EVALUATIONSHEET_INVALID");
            if (!Class.Students.Any(s => s.Id == student.Id))
                throw new ErrorException("Student does not belong to this class.", "STUDENT_NOT_IN_CLASS");
            if (!_questionIds.Contains(questionId))
                throw new ErrorException("Question is not part of this sheet.", "QUESTION_NOT_USED");
        }

        #endregion
    }
}

