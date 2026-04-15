using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.Tests;

public class SchoolTests
{

    #region Creational Tests
    [Theory]
    [InlineData("Test School")]
    public void Given_Valid_Data_Then_Create_Success(string title)
    {
        // Act
        var school = School.Create(title, []);

        // Assert
        Assert.NotNull(school);
    }

    #endregion


    #region Title Tests

    [Theory]
    [MemberData(nameof(NotUniqueTitleData))]
    public void Given_Not_Unique_Title_Then_Throw_ArgumentException(string title, IEnumerable<School> otherSchools)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => School.Create(title, otherSchools));
    }

    [Theory]
    [MemberData(nameof(UniqueTitleData))]
    public void Given_Unique_Title_Then_Create_Success(string title, IEnumerable<School> otherSchools)
    {
        // Act
        var school = School.Create(title, otherSchools);

        // Assert
        Assert.NotNull(school);
    }

    [Fact]
    public void Given_WhiteSpace_SchoolTitle_Then_Throw_ArgumentException()
    {
        // Arrange
        var whiteSpaceString = " ";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => School.Create(whiteSpaceString, []));
    }

    [Fact]
    public void Given_Null_SchoolTitle_Then_Throw_ArgumentException()
    {
        // Arrange
        string? nullString = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => School.Create(nullString!, []));
    }

    [Fact]
    public void Given_SchoolTitle_With_Length_Over_50_Then_Throw_ArgumentException()
    {
        // Arrange
        var longString = new string('a', 51);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => School.Create(longString, []));
    }
    #endregion


    #region AddClass Tests

    [Theory]
    [MemberData(nameof(CreateWithValidData))]
    public void Given_Valid_ClassData_When_AddClass_Then_Success(string classTitle, DateOnly startDate, DateOnly endDate)
    {
        // Arrange
        var school = School.Create("Test School", []);
        IEnumerable<Class> otherClasses = [];
        var expected = 1;

        // Act
        school.AddClass(classTitle, startDate, endDate, otherClasses);

        // Assert
        Assert.Equal(expected, school.Classes.Count);
    }

    #endregion

    #region Member Data

    public static IEnumerable<object[]> CreateWithValidData()
    {
        yield return new object[]
        {
            "Test Class",
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(3))
        };
    }

    public static IEnumerable<object[]> NotUniqueTitleData()
    {
        var otherSchools = GetOtherSchools();
        yield return new object[]
        {
            "NotUniqueTitle",
            otherSchools
        };
    }

    public static IEnumerable<object[]> UniqueTitleData()
    {
        var otherSchools = GetOtherSchools();
        yield return new object[]
        {
            "VeryUniqueName",
            otherSchools
        };
    }

    private static IEnumerable<School> GetOtherSchools()
    {
        return
        [
            School.Create("NotUniqueTitle", []),
            School.Create("UniqueTitle", [])
        ];
    }

    #endregion
}
