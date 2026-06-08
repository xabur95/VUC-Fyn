using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Entities
{
  /// <summary>
  /// Aggregate root representing a Question. See class-level remarks for invariants.
  /// </summary>
  public class Question : Entity
  {
    #region Properties

    public Title Title { get; private set; } = null!;
    public string Text { get; private set; } = null!;
    public int Points { get; private set; }
    public ActiveStatus ActiveStatus { get; private set; }
    public Question? ParentQuestion { get; private set; }
    public Answer? Answer { get; private set; }

    private readonly List<Tag> _tags = new();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    private readonly List<Subject> _subjects = new();
    public IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();

    public Guid CreatedByUserId { get; private set; }

    #endregion

    #region Constructors

    private Question() { }

    private Question(Title title, string text, int points, ActiveStatus activeStatus, Guid createdByUserId)
    {
      Title = title;
      Text = text;
      Points = points;
      ActiveStatus = activeStatus;
      CreatedByUserId = createdByUserId;
    }

    #endregion

    #region Factory

    public static Question Create(
        Teacher creator,
        Title title,
        string text,
        int points,
        ActiveStatus activeStatus,
        Question? parentQuestion = null,
        IEnumerable<Tag>? tags = null,
        IEnumerable<Subject>? subjects = null)
    {
      EnsureUserCanCreate(creator);
      EnsurePoints(points);

      var question = new Question(title, text ?? string.Empty, points, activeStatus, creator.Id);

      if (parentQuestion is not null)
        question.SetParentQuestionInternal(parentQuestion);

      if (tags is not null)
      {
        foreach (var tag in tags)
          question.AddTagInternal(tag);
      }

      if (subjects is not null)
      {
        foreach (var subject in subjects)
          question.AddSubjectInternal(subject);
      }

      return question;
    }

    #endregion

    #region Behavior – mutations gated by ownership

    public void Update(Teacher editor, Title title, string text, int points, ActiveStatus activeStatus)
    {
      EnsureIsOwner(editor);
      EnsurePoints(points);

      Title = title;
      Text = text;
      Points = points;
      ActiveStatus = activeStatus;
    }

    public void UpdateActiveStatus(Teacher editor, ActiveStatus activeStatus)
    {
      EnsureIsOwner(editor);
      ActiveStatus = activeStatus;
    }

    public void AddTag(Teacher editor, Tag tag)
    {
      EnsureIsOwner(editor);
      AddTagInternal(tag);
    }

    public void RemoveTag(Teacher editor, Tag tag)
    {
      EnsureIsOwner(editor);
      if (tag is null) return;
      _tags.RemoveAll(t => t.Equals(tag));
    }

    public void AddSubject(Teacher editor, Subject subject)
    {
      EnsureIsOwner(editor);
      AddSubjectInternal(subject);
    }

    public void RemoveSubject(Teacher editor, Subject subject)
    {
      EnsureIsOwner(editor);
      if (subject is null) return;
      _subjects.RemoveAll(s => s.Equals(subject));
    }

    public void SetParentQuestion(Teacher editor, Question? parent)
    {
      EnsureIsOwner(editor);
      SetParentQuestionInternal(parent);
    }

    public void ClearParentQuestion(Teacher editor)
    {
      EnsureIsOwner(editor);
      ParentQuestion = null;
    }

    #endregion

    #region Answer lifecycle (Question is the aggregate root)

    public Answer SetAnswer(Teacher editor, Title answerTitle, string answerText)
    {
      EnsureIsOwner(editor);
      if (Answer is not null)
        throw new ErrorException("Answer already exists. Use UpdateAnswer to modify it.", "QUESTION_ANSWER_EXISTS");

      Answer = new Answer(Id, answerTitle, answerText, editor.Id);
      return Answer;
    }

    public void UpdateAnswer(Teacher editor, Title answerTitle, string answerText)
    {
      EnsureIsOwner(editor);
      if (Answer is null)
        throw new ErrorException("No answer exists to update.", "QUESTION_ANSWER_MISSING");

      Answer.UpdateContent(answerTitle, answerText);
    }

    public void RemoveAnswer(Teacher editor)
    {
      EnsureIsOwner(editor);
      Answer = null;
    }

    #endregion

    #region Invariant helpers

    private void AddTagInternal(Tag tag)
    {
      if (tag is null)
        throw new ErrorException("Tag cannot be null.", "QUESTION_INVALID");
      if (_tags.Any(t => t.Equals(tag))) return; // idempotent
      _tags.Add(tag);
    }

    private void AddSubjectInternal(Subject subject)
    {
      if (subject is null)
        throw new ErrorException("Subject cannot be null.", "QUESTION_INVALID");
      if (_subjects.Any(s => s.Equals(subject))) return; // idempotent
      _subjects.Add(subject);
    }

    private void SetParentQuestionInternal(Question? parent)
    {
      if (parent is null)
      {
        ParentQuestion = null;
        return;
      }

      if (ReferenceEquals(parent, this) || parent.Equals(this))
        throw new ErrorException("A question cannot be its own parent.", "QUESTION_INVALID");

      // Walk up the chain to prevent cycles.
      var current = parent.ParentQuestion;
      while (current is not null)
      {
        if (current.Equals(this))
          throw new ErrorException("Setting this parent would create a cycle.", "QUESTION_INVALID");
        current = current.ParentQuestion;
      }

      ParentQuestion = parent;
    }

    private static void EnsureUserCanCreate(Teacher creator)
    {
      if (creator is null)
        throw new ErrorException("Creator must be provided.", "QUESTION_INVALID");
      if (!creator.IsActive)
        throw new ErrorException("Inactive users cannot create questions.", "QUESTION_FORBIDDEN");
      if (!creator.Roles.Any(r => r.RoleType == RoleType.Teacher))
        throw new ErrorException("Only users with the Teacher role can create questions.", "QUESTION_FORBIDDEN");
    }

    private void EnsureIsOwner(Teacher user)
    {
      if (user is null)
        throw new ErrorException("User must be provided.", "QUESTION_INVALID");
      if (user.Id != CreatedByUserId)
        throw new ErrorException("Only the user who created the question can modify it.", "QUESTION_FORBIDDEN");
    }

    private static void EnsurePoints(int points)
    {
      if (points < 0)
        throw new ErrorException("Points must be zero or positive.", "QUESTION_INVALID");
    }

    #endregion
  }
}
