using System.Collections;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.ValueObjects;
/// <summary>
/// Author: Michael
/// Unit tests for Name class constructor validering med forskellige input scenarier.
/// Verificerer at validerings regler håndhæves og exceptions kastes ved ugyldigt input.
/// </summary>
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


    [Theory]
    [ClassData(typeof(InvalidNameData))]
    public void Constructor_WhenInvalidNameIsUsed_ThrowsArgumentException(string firstName, string lastName)
    {
        // Act
        var result = () => new Name(firstName, lastName);

        // Assert
        Assert.Throws<ArgumentException>(result);
    }
}

public class InvalidNameData : IEnumerable<object[]>
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "", "Hansen" };
        yield return new object[] { "Peter123", "Hansen" };
        yield return new object[] { "P", "Hansen" };
    }
}