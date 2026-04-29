using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.ValueObjects;
/// <summary>
/// Author: Michael
/// Unit tests for the Email class that verify handling of valid and invalid email addresses.
/// Tests that an ArgumentException is thrown for invalid emails and that valid emails are created successfully.
/// </summary>
public class EmailTests
{
    public static IEnumerable<object[]> ValidEmailData =>
        new List<object[]>
        {
            new object[] {"bob@gmail.com"},
            new object[] {"poul@bundgaard.dk"},
            new object[] {"homer@simpson.com"}
        };

    [Theory]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("@gmail.com")]
    [InlineData("test@gmail")]
    [InlineData("testgmail.com")]
    public void Constructor_WhenEmailIsInvalid_ThrowsErrorException(string email)
    {
        // Act
        var act = () => new Email(email);

        // Assert
        Assert.Throws<ErrorException>(act);
    }

    [Fact]
    public void Constructor_WhenEmailIsEmpty_ThrowsErrorException()
    {
        // Arrange
        var email = "";

        // Act
        var act = () => new Email(email);

        // Assert
        Assert.Throws<ErrorException>(act);
    }

    [Theory]
    [MemberData(nameof(ValidEmailData))]
    public void Constructor_WhenEmailIsValid_ShouldCreateEmail(string email)
    {
        // Act
        var result = new Email(email);

        // Assert
        Assert.Equal(email, result.Value);
    }
}