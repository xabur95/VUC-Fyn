using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Reqnroll;
using Semesterprojekt1PBA.Application.Dto.Question.Query;
using Semesterprojekt1PBA.Application.Features.Question.Command;
using Semesterprojekt1PBA.Application.Features.Question.Query;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Features.StepDefinitions;

[Binding]
public class TeacherQuestionOverviewSteps
{
    private Teacher _teacher = null!;
    private readonly List<Question> _questions = new();
    private IEnumerable<GetQuestionResponse> _overviewResult = Enumerable.Empty<GetQuestionResponse>();

    private readonly Mock<IQuestionRepository> _questionRepoMock = new();
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<ITagRepository> _tagRepoMock = new();
    private readonly Mock<ISubjectRepository> _subjectRepoMock = new();

    [Given(@"a teacher ""(.*)"" with email ""(.*)"" exists")]
    public void GivenATeacherWithEmailExists(string fullName, string email)
    {
        var names = fullName.Split(' ', 2);
        _teacher = Teacher.Create(names[0], names[1], email, "Password1", []);

        _userRepoMock
            .Setup(r => r.GetByIdAsync<Teacher>(_teacher.Id))
            .ReturnsAsync(_teacher);
    }

    [Given(@"the following questions exist for the teacher")]
    public void GivenTheFollowingQuestionsExistForTheTeacher(DataTable table)
    {
        foreach (var row in table.Rows)
        {
            var question = Question.Create(
                _teacher,
                row["Title"],
                row["Text"],
                int.Parse(row["Points"]),
                ActiveStatus.Active);

            _questions.Add(question);
        }

        SetupRepositoryGetAll();
    }

    [When(@"the teacher views the question overview")]
    public async Task WhenTheTeacherViewsTheQuestionOverview()
    {
        SetupRepositoryGetAll();

        var handler = new GetAllQuestionsQueryHandler(
            Mock.Of<ILogger<GetAllQuestionsQueryHandler>>(),
            _questionRepoMock.Object);

        _overviewResult = await handler.Handle(new GetAllQuestionsQuery(), CancellationToken.None);
    }

    [When(@"the teacher creates a new question with title ""(.*)"" and text ""(.*)"" and points (.*)")]
    public async Task WhenTheTeacherCreatesANewQuestion(string title, string text, int points)
    {
        _questionRepoMock
            .Setup(r => r.CreateQuestionAsync(It.IsAny<Question>()))
            .Callback<Question>(q => _questions.Add(q))
            .Returns(Task.CompletedTask);

        var request = new Application.Dto.Question.Command.CreateQuestionRequest(
            _teacher.Id, title, text, points, ActiveStatus.Active, null, null, null);

        var handler = new CreateQuestionCommandHandler(
            Mock.Of<ILogger<CreateQuestionCommandHandler>>(),
            _questionRepoMock.Object,
            _userRepoMock.Object,
            _tagRepoMock.Object,
            _subjectRepoMock.Object);

        await handler.Handle(new CreateQuestionCommand(request), CancellationToken.None);
    }

    [Then(@"the overview should contain (.*) questions")]
    public void ThenTheOverviewShouldContainQuestions(int expectedCount)
    {
        _overviewResult.Count().Should().Be(expectedCount);
    }

    [Then(@"the overview should contain a question with title ""(.*)""")]
    public void ThenTheOverviewShouldContainAQuestionWithTitle(string expectedTitle)
    {
        _overviewResult.Should().Contain(q => q.Title == expectedTitle);
    }

    private void SetupRepositoryGetAll()
    {
        _questionRepoMock
            .Setup(r => r.GetAllQuestionsAsync())
            .ReturnsAsync(_questions.AsEnumerable());
    }
}
