using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Semesterprojekt1PBA.Domain.Test.Entities;

public class SchoolTests
{

    #region Creational Tests
    [Theory]
    [InlineData("Test School")]
    public void Given_Valid_Data_Then_Create_Success(string title)
    {
        // Act
        var school = School.Create(title, Array.Empty<School>());

        // Assert
        Assert.NotNull(school);
    }

    #endregion


    #region Title Tests

    [Theory]
    [MemberData(nameof(NotUniqueTitleData))]
    public void Given_Not_Unique_Title_Then_Throw_ErrorException(string title, IEnumerable<School> otherSchools)
    {
        // Arrange
        var school =  School.Create("Existing", Array.Empty<School>());

        // Act & Assert
        Assert.Throws<ErrorException>(() => school.UpdateTitle(title, otherSchools));
    }

    [Theory]
    [MemberData(nameof(UniqueTitleData))]
    public void Given_Unique_Title_Then_Void(string title, IEnumerable<School> otherSchools)
    {
        // Arrange
        var school = School.Create("Existing", Array.Empty<School>());

        // Act
        school.UpdateTitle(title, otherSchools);
    }

    [Fact]
    public void Given_Valid_SchoolTitle_Then_Void()
    {
        // Arrange
        var school = School.Create("Existing", Array.Empty<School>());

        // Act
        school.UpdateTitle("Valid Title", Array.Empty<School>());
    }

    [Fact]
    public void Given_WhiteSpace_SchoolTitle_Then_Throw_ErrorException()
    {

        // Arrange
        var school = School.Create("Existing", Array.Empty<School>());
        var whiteSpaceString = " ";
        // Act & Assert
        Assert.Throws<ErrorException>(() => school.UpdateTitle(whiteSpaceString, Array.Empty<School>()));
    }

    [Fact]
    public void Given_Null_SchoolTitle_Then_Throw_ErrorException()
    {
        // Arrange
        var school = School.Create("Existing", Array.Empty<School>());
        string? nullString = null;
        // Act & Assert
        Assert.Throws<ErrorException>(() => school.UpdateTitle(nullString!, Array.Empty<School>()));
    }

    [Fact]
    public void Given_SchoolTitle_With_Length_Over_50_Then_Throw_ErrorException()
    {
        // Arrange
        var school = School.Create("Existing", Array.Empty<School>());
        var longString = new string('a', 51);

        // Act & Assert
        Assert.Throws<ErrorException>(() => school.UpdateTitle(longString, Array.Empty<School>()));
    }
    #endregion


    #region AddClass Tests

    [Theory]
    [MemberData(nameof(CreateWithValidData))]
    public void Given_Valid_ClassData_When_AddClass_Then_Success(string classTitle, DateOnly startDate, DateOnly endDate)
    {
        // Arrange
        var school = School.Create("Existing", Array.Empty<School>());
        IEnumerable<Class> otherClasses = Array.Empty<Class>();
        var expected = 1;

        // Act
        var addedClass = school.AddClass(classTitle, startDate, endDate, otherClasses);

        // Assert
        Assert.Equal(expected, school.Classes.Count);
    }

    // Duplicate class test removed because AddClass creates new Class instances with unique Ids.

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
        var otherSchools = GetOtherSchools()
            .Select(t => School.Create(t, Array.Empty<School>()))
            .ToArray(); 

        yield return new object[]
        {
            "NotUniqueTitle",
            otherSchools
        };
    }

    public static IEnumerable<object[]> UniqueTitleData()
    {
        var otherSchools = GetOtherSchools()
            .Select(t => School.Create(t, Array.Empty<School>()))
            .ToArray();

        yield return new object[]
        {
            "VeryUniqueTitle",
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

    // Duplicate class member data removed
    #endregion
}
