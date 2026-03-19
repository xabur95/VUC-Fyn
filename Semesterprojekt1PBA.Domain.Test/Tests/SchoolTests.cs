using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Test.Fakes;
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
        var school = Entities.School.Create(title, []);

        // Assert
        Assert.NotNull(school);
    }

    #endregion


    #region Title Tests

    [Theory]
    [MemberData(nameof(NotUniqueTitleData))]
    public void Given_Not_Unique_Title_Then_Throw_ArgumentException(Title title, IEnumerable<string> otherSchools)
    {
        // Arrange
        var school = new FakeSchool();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => school.SetScoolTitle(title, otherSchools));
    }

    [Theory]
    [MemberData(nameof(UniqueTitleData))]
    public void Given_Unique_Title_Then_Void(Title title, IEnumerable<string> otherSchools)
    {
        // Arrange
        var school = new FakeSchool();

        // Act
        school.SetScoolTitle(title, otherSchools);
    }

    [Fact]
    public void Given_Valid_SchoolTitle_Then_Void()
    {
        // Arrange
        var school = new FakeSchool();

        // Act
        school.SetScoolTitle("Valid Title", []);
    }

    [Fact]
    public void Given_WhiteSpace_SchoolTitle_Then_Throw_ArgumentException()
    {

        // Arrange
        var school = new FakeSchool();
        var whiteSpaceString = " ";
        // Act & Assert
        Assert.Throws<ArgumentException>(() => school.SetScoolTitle(whiteSpaceString, []));
    }

    [Fact]
    public void Given_Null_SchoolTitle_Then_Throw_ArgumentException()
    {
        // Arrange
        var school = new FakeSchool();
        string? nullString = null;
        // Act & Assert
        Assert.Throws<ArgumentException>(() => school.SetScoolTitle(nullString!, []));
    }

    [Fact]
    public void Given_SchoolTitle_With_Length_Over_50_Then_Throw_ArgumentException()
    {
        // Arrange
        var school = new FakeSchool();
        var longString = new string('a', 51);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => school.SetScoolTitle(longString, []));
    }
    #endregion


    #region AddClass Tests

    [Theory]
    [MemberData(nameof(CreateWithValidData))]
    public void Given_Valid_ClassData_When_AddClass_Then_Success(string classTitle, DateOnly startDate, DateOnly endDate)
    {
        // Arrange
        var school = new FakeSchool();
        IEnumerable<Class> otherClasses = [];
        var expected = 1;

        // Act
        var addedClass = school.AddClass(classTitle, startDate, endDate, otherClasses);

        // Assert
        Assert.Equal(school.Classes.Count, expected);
    }

    [Theory]
    [MemberData(nameof(DuplicateClassData))]
    public void Given_Duplicate_ClassData_When_AddClass_Then_Throw_ArgumentException(Class fakeClass, IEnumerable<Class> otherClasses)
    {
        // Arrange
        var school = new FakeSchool();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => school.AssureNoDuplicateClass(fakeClass, otherClasses.ToList()));
    }

    #endregion

    #region Memeber Data

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
            "NotUniqueName",
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

    private static IEnumerable<string> GetOtherSchools()
    {
        return new[]
        {
            "NotUniqueTitle",
            "UniqueTitle"
        };
    }

    public static IEnumerable<object[]> DuplicateClassData()
    {
        var otherClasses = GetOtherClasses();
        yield return new object[]
        {
            new FakeClass(Guid.Parse("80d1ffdd-b31d-4183-bcc6-6709e7177de7"), "TestClass"),
            otherClasses
        };
    }
    private static IEnumerable<FakeClass> GetOtherClasses()
    {
        return
        [
            new FakeClass(Guid.Parse("80d1ffdd-b31d-4183-bcc6-6709e7177de7"), "TestClass"),
            new FakeClass(Guid.Parse("501b49fd-9428-456e-8fe7-95c24bbc8a88"), "OtherTestClass")
        ];
    }
    #endregion
}
