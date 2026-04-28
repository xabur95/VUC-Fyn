using FluentAssertions;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Entities;

/// <summary>
/// Unit tests for the <see cref="Answer"/> entity.
/// Answer is part of the <see cref="Question"/> aggregate, so all behaviors
/// are exercised through the aggregate root.
/// </summary>
public class AnswerTests
{
    private static User NewTeacher(string email = "teach@vucfyn.dk") =>
        User.Create("Tina", "Teacher", email, RoleType.Teacher);

    private static Question NewQuestion(User creator) =>
        Question.Create(creator, "Q", "Text", 10, ActiveStatus.Active);

    [Fact]
    public void Answer_CreatedThroughAggregate_ShouldHaveExpectedState()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);

        var answer = question.SetAnswer(teacher, "Title", "Body text");

        answer.Should().NotBeNull();
        answer.QuestionId.Should().Be(question.Id);
        answer.CreatedByUserId.Should().Be(teacher.Id);
        answer.Title.Value.Should().Be("Title");
        answer.Text.Should().Be("Body text");
    }

    [Fact]
    public void Answer_CannotBeConstructedFromOutsideDomain()
    {
        // The Answer constructor is `internal`, so the test project (which lives
        // in a separate assembly) cannot call it directly. We assert that fact
        // with reflection: there must be no *public* constructor.
        var publicCtors = typeof(Answer).GetConstructors();

        publicCtors.Should().BeEmpty(
            "Answer must only be created through the Question aggregate root.");
    }

    [Fact]
    public void UpdateAnswer_WithEmptyText_ShouldThrow()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        question.SetAnswer(teacher, "T", "Initial text");

        var act = () => question.UpdateAnswer(teacher, "T", "   ");

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("ANSWER_INVALID");
    }

    [Fact]
    public void UpdateAnswer_WithValidContent_ShouldUpdateInPlace()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        var answer = question.SetAnswer(teacher, "Old", "Old text");

        question.UpdateAnswer(teacher, "New", "New text");

        // Same instance is mutated; identity preserved.
        question.Answer.Should().BeSameAs(answer);
        answer.Title.Value.Should().Be("New");
        answer.Text.Should().Be("New text");
    }

    [Fact]
    public void SetAnswer_WithEmptyText_ShouldThrow()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);

        var act = () => question.SetAnswer(teacher, "T", "");

        act.Should().Throw<ErrorException>()
           .Which.ErrorCode.Should().Be("ANSWER_INVALID");
    }

    [Fact]
    public void RemoveAnswer_ShouldDetachAnswerFromQuestion()
    {
        var teacher = NewTeacher();
        var question = NewQuestion(teacher);
        question.SetAnswer(teacher, "T", "Text");

        question.RemoveAnswer(teacher);

        question.Answer.Should().BeNull();
    }

    [Fact]
    public void TwoAnswers_OnDifferentQuestions_ShouldHaveDifferentIds()
    {
        var teacher = NewTeacher();
        var q1 = NewQuestion(teacher);
        var q2 = Question.Create(teacher, "Other", "Text", 1, ActiveStatus.Active);

        var a1 = q1.SetAnswer(teacher, "A1", "Text 1");
        var a2 = q2.SetAnswer(teacher, "A2", "Text 2");

        a1.Id.Should().NotBe(a2.Id);
        a1.QuestionId.Should().Be(q1.Id);
        a2.QuestionId.Should().Be(q2.Id);
    }
}
