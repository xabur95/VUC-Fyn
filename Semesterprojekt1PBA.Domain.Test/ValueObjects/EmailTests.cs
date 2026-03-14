using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.ValueObjects;

public class EmailTests
{
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


}