using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.ValueObjects;

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
    public void Constructor_WhenEmailIsInvalid_ThrowsArgumentException(string email)
    {
        // Act
        var act = () => new Email(email);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_WhenEmailIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var email = "";

        // Act
        var act = () => new Email(email);

        // Assert
        Assert.Throws<ArgumentException>(act);
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