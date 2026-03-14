using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.ValueObjects;

public class NameTests
{
    [Fact]
    public void Constructor_WhenFirstNameIsEmpty_ThrowsArgumentException()
    {
        // Arange
        var firstName = "";

        // Act
        var act = () => new Name(firstName, "Hansen");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_WhenFirstNameContainsNumbers_ThrowsArgumentException()
    {
        // Arange
        var firstName = "Peter123";
        // Act
        var act = () => new Name(firstName, "Hansen");
        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData("P")]
    [InlineData("Pppppppppppppppppppppppppppppppppppppppppppp")]
    public void Constructor_WhenFirstNameLengthIsOutOfArea_ThrowsArgumentException(string firstName)
    {
        // Act
        var act = () => new Name(firstName, "Hansen");
        // Assert
        Assert.Throws<ArgumentException>(act);
    }


    [Fact]
    public void Constructor_WhenLastNameIsEmpty_ThrowsArgumentException()
    {
        // Arange
        var lastName = "";

        // Act
        var act = () => new Name("Peter", lastName);
        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_WhenLastNameContainsNumbers_ThrowsArgumentException()
    {
        // Arange
        var lastName = "Hansen123";
        // Act
        var act = () => new Name("Peter", lastName);
        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData("P")]
    [InlineData("Pppppppppppppppppppppppppppppppppppppppppppp")]
    public void Constructor_WhenLastNameLengthIsOutOfArea_ThrowsArgumentException(string lastName)
    {
        // Act
        var act = () => new Name("Peter", lastName);
        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}