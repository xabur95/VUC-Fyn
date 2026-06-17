using FluentAssertions;
using Moq;
using Reqnroll;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjectsAndEnums;

namespace Semesterprojekt1PBA.Domain.Test.Features.StepDefinitions;

[Binding]
public class AdminSchoolOverviewSteps
{
    // DIP vi afhænger af interface, ikke konkret implementation
    private readonly Mock<ISchoolRepository> _repoMock = new();
    private IEnumerable<School> _result = [];

    [Given("the system has {int} registered schools")]
    public void GivenSchoolsExist(int count)
    {
        var schools = Enumerable.Range(1, count)
            .Select(i => School.Create((Title)$"School {i}", []))
            .ToList();

        // Stub der returnerer fake data uden rigtig database
        _repoMock.Setup(r => r.GetAllSchoolsAsync()).ReturnsAsync(schools);
    }

    [When("the admin requests the school overview")]
    public async Task WhenAdminRequestsOverview()
    {
        // ReqnRoll binder metode til When i feature fil
        _result = await _repoMock.Object.GetAllSchoolsAsync();
    }

    [Then("the overview should contain {int} schools")]
    public void ThenOverviewContains(int expected)
    {
        _result.Should().HaveCount(expected);
    }
}