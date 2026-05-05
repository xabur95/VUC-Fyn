using FluentAssertions;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Entities;

/// <summary>
/// Unit tests for the <see cref="Question"/> aggregate root.
/// </summary>
public class QuestionTests
{
    // ---------- helpers ----------

    private static User NewTeacher(string email = "teach@vucfyn.dk") =>
        Teacher.Create("Tina", "Teacher", email);

    private static User NewStudent(string email = "stud@vucfyn.dk") =>
        Student.Create("Sam", "Student", email, "12345", DateOnly.FromDateTime(DateTime.Now), null);

    private static Question NewQuestion(User? creator = null) =>
        Question.Create(
            creator ?? NewTeacher(),
            title: "What is DDD?",
            text: "Explain Domain-Driven Design.",
            points: 10,
            activeStatus: ActiveStatus.Active);

    // ---------- Create ----------

    [Fact]
    public void Create_WithValidTeacher_ShouldInitializeQuestion()
    {
        var teacher = NewTeacher();

        var question = Question.Create(
            teacher,
            "Title",
            "Some text",
            10,
            ActiveStatus.Active);

        question.Should().NotBeNull();
        question.Title.Value.Should().Be("Title");
        question.Text.Should().Be("Some text");
        question.Points.Should().Be(10);
        question.ActiveStatus.Should().Be(ActiveStatus.Active);
        question.CreatedByUserId.Should().Be(teacher.Id);
        question.Tags.Should().BeEmpty();
        question.Subjects.Should().BeEmpty();
        question.Answer.Should().BeNull();
        question.ParentQuestion.Should().BeNull();
    }

    [Fact]
    public void Create_WhenCreatorIsStudent_ShouldThrow()
    {
        var student = NewStudent();

        var act = () => Question.Create(student, "T", "Text", 1, ActiveStatus.Active);

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_FORBIDDEN");
    }

    [Fact]
    public void Create_WhenCreatorIsInactive_ShouldThrow()
    {
        var teacher = NewTeacher();
        teacher.Deactivate();

        var act = () => Question.Create(teacher, "T", "Text", 1, ActiveStatus.Active);

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_FORBIDDEN");
    }

    [Fact]
    public void Create_WithNegativePoints_ShouldThrow()
    {
        var teacher = NewTeacher();

        var act = () => Question.Create(teacher, "T", "Text", -1, ActiveStatus.Active);

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_INVALID");
    }

    [Fact]
    public void Create_WithParentTagsAndSubjects_ShouldPopulateCollections()
    {
        var teacher = NewTeacher();
        var parent = NewQuestion(teacher);
        var tag = Tag.Create("OOP", "Object-oriented");
        var subject = Subject.Create("Math", Level.A);

        var question = Question.Create(
            teacher,
            "Child",
            "Text",
            5,
            ActiveStatus.Active,
            parentQuestion: parent,
            tags: new[] { tag },
            subjects: new[] { subject });

        question.ParentQuestion.Should().BeSameAs(parent);
        question.Tags.Should().ContainSingle().Which.Should().Be(tag);
        question.Subjects.Should().ContainSingle().Which.Should().Be(subject);
    }

    // ---------- Update ----------

    [Fact]
    public void Update_ByOwner_ShouldChangeFields()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);

        question.Update(teacher, "New title", "New text", 20, ActiveStatus.Inactive);

        question.Title.Value.Should().Be("New title");
        question.Text.Should().Be("New text");
        question.Points.Should().Be(20);
        question.ActiveStatus.Should().Be(ActiveStatus.Inactive);
    }

    [Fact]
    public void Update_ByDifferentUser_ShouldThrow()
    {
        var owner = NewTeacher("owner@vucfyn.dk");
        var other = NewTeacher("other@vucfyn.dk");
        var question = NewQuestion(owner);

        var act = () => question.Update(other, "x", "y", 1, ActiveStatus.Active);

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_FORBIDDEN");
    }

    [Fact]
    public void Update_WithNegativePoints_ShouldThrow()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);

        var act = () => question.Update(teacher, "x", "y", -5, ActiveStatus.Active);

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_INVALID");
    }

    // ---------- Tags ----------

    [Fact]
    public void AddTag_ByOwner_ShouldAddTag()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        var tag = Tag.Create("DDD", "Domain-driven design");

        question.AddTag(teacher, tag);

        question.Tags.Should().ContainSingle().Which.Should().Be(tag);
    }

    [Fact]
    public void AddTag_Twice_ShouldBeIdempotent()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        var tag = Tag.Create("DDD", "Domain-driven design");

        question.AddTag(teacher, tag);
        question.AddTag(teacher, tag);

        question.Tags.Should().HaveCount(1);
    }

    [Fact]
    public void RemoveTag_ByOwner_ShouldRemoveTag()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        var tag = Tag.Create("DDD", "Domain-driven design");
        question.AddTag(teacher, tag);

        question.RemoveTag(teacher, tag);

        question.Tags.Should().BeEmpty();
    }

    [Fact]
    public void AddTag_ByNonOwner_ShouldThrow()
    {
        var owner = NewTeacher("owner@vucfyn.dk");
        var other = NewTeacher("other@vucfyn.dk");
        var question = NewQuestion(owner);
        var tag = Tag.Create("X", "x");

        var act = () => question.AddTag(other, tag);

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_FORBIDDEN");
    }

    // ---------- Subjects ----------

    [Fact]
    public void AddSubject_ByOwner_ShouldAddSubject()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        var subject = Subject.Create("Physics", Level.B);

        question.AddSubject(teacher, subject);

        question.Subjects.Should().ContainSingle().Which.Should().Be(subject);
    }

    [Fact]
    public void AddSubject_Twice_ShouldBeIdempotent()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        var subject = Subject.Create("Physics", Level.B);

        question.AddSubject(teacher, subject);
        question.AddSubject(teacher, subject);

        question.Subjects.Should().HaveCount(1);
    }

    [Fact]
    public void RemoveSubject_ByOwner_ShouldRemoveSubject()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        var subject = Subject.Create("Physics", Level.B);
        question.AddSubject(teacher, subject);

        question.RemoveSubject(teacher, subject);

        question.Subjects.Should().BeEmpty();
    }

    // ---------- ParentQuestion ----------

    [Fact]
    public void SetParentQuestion_ByOwner_ShouldAssignParent()
    {
        var teacher = NewTeacher();
        var parent = NewQuestion(teacher);
        var child = NewQuestion(teacher);

        child.SetParentQuestion(teacher, parent);

        child.ParentQuestion.Should().BeSameAs(parent);
    }

    [Fact]
    public void SetParentQuestion_ToNull_ShouldDetach()
    {
        var teacher = NewTeacher();
        var parent = NewQuestion(teacher);
        var child = Question.Create(teacher, "C", "T", 1, ActiveStatus.Active, parentQuestion: parent);

        child.SetParentQuestion(teacher, null);

        child.ParentQuestion.Should().BeNull();
    }

    [Fact]
    public void SetParentQuestion_ToSelf_ShouldThrow()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);

        var act = () => question.SetParentQuestion(teacher, question);

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_INVALID");
    }

    [Fact]
    public void SetParentQuestion_WhenCreatesCycle_ShouldThrow()
    {
        var teacher = NewTeacher();
        var grandparent = NewQuestion(teacher);
        var parent = Question.Create(teacher, "P", "T", 1, ActiveStatus.Active, parentQuestion: grandparent);
        var child = Question.Create(teacher, "C", "T", 1, ActiveStatus.Active, parentQuestion: parent);

        // Trying to set grandparent.ParentQuestion = child would create a cycle
        var act = () => grandparent.SetParentQuestion(teacher, child);

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_INVALID");
    }

    [Fact]
    public void ClearParentQuestion_ByOwner_ShouldDetach()
    {
        var teacher = NewTeacher();
        var parent = NewQuestion(teacher);
        var child = Question.Create(teacher, "C", "T", 1, ActiveStatus.Active, parentQuestion: parent);

        child.ClearParentQuestion(teacher);

        child.ParentQuestion.Should().BeNull();
    }

    // ---------- Answer lifecycle ----------

    [Fact]
    public void SetAnswer_ByOwner_ShouldCreateAndAttachAnswer()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);

        var answer = question.SetAnswer(teacher, "Answer", "Because reasons.");

        question.Answer.Should().NotBeNull().And.BeSameAs(answer);
        answer.QuestionId.Should().Be(question.Id);
        answer.CreatedByUserId.Should().Be(teacher.Id);
    }

    [Fact]
    public void SetAnswer_WhenAnswerAlreadyExists_ShouldThrow()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        question.SetAnswer(teacher, "A", "First");

        var act = () => question.SetAnswer(teacher, "B", "Second");

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_ANSWER_EXISTS");
    }

    [Fact]
    public void SetAnswer_ByNonOwner_ShouldThrow()
    {
        var owner = NewTeacher("owner@vucfyn.dk");
        var other = NewTeacher("other@vucfyn.dk");
        var question = NewQuestion(owner);

        var act = () => question.SetAnswer(other, "A", "Text");

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_FORBIDDEN");
    }

    [Fact]
    public void UpdateAnswer_ByOwner_ShouldChangeAnswerContent()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        question.SetAnswer(teacher, "Old", "Old text");

        question.UpdateAnswer(teacher, "New", "New text");

        question.Answer!.Title.Value.Should().Be("New");
        question.Answer!.Text.Should().Be("New text");
    }

    [Fact]
    public void UpdateAnswer_WhenNoAnswerExists_ShouldThrow()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);

        var act = () => question.UpdateAnswer(teacher, "T", "Text");

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("QUESTION_ANSWER_MISSING");
    }

    [Fact]
    public void RemoveAnswer_ByOwner_ShouldDetachAnswer()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        question.SetAnswer(teacher, "T", "Text");

        question.RemoveAnswer(teacher);

        question.Answer.Should().BeNull();
    }
}
