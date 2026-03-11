namespace Semesterprojekt1PBA.Domain.Test.Entities.User;

public class UserDomainModelTest
{
    [Fact]
    public void Constructor_WhenFirstNameIsEmpty_ThrowsArgumentException()
    {
        // Arange
        var firstName = "";

        // Act
        var act = () => new TestUser(firstName, "Hansen", "test@test.com");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_WhenFirstNameContainsNumbers_ThrowsArgumentException()
    {
        // Arange
        var firstName = "Peter123";
        // Act
        var act = () => new TestUser(firstName, "Hansen", "test@test.com");
        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData("P")]
    [InlineData("Pppppppppppppppppppppppppppppppppppppppppppp")]
    public void Constructor_WhenFirstNameLengthIsOutOfArea_ThrowsArgumentException(string firstName)
    {
        // Act
        var act = () => new TestUser(firstName, "Hansen", "test@test.com");
        // Assert
        Assert.Throws<ArgumentException>(act);
    }


    [Fact]
    public void Constructor_WhenLastNameIsEmpty_ThrowsArgumentException()
    {
        // Arange
        var lastName = "";

        // Act
        var act = () => new TestUser("Peter", lastName, "test@test.com");
        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_WhenLastNameContainsNumbers_ThrowsArgumentException()
    {
        // Arange
        var lastName = "Hansen123";
        // Act
        var act = () => new TestUser("Peter", lastName, "test@test.com");
        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData("P")]
    [InlineData("Pppppppppppppppppppppppppppppppppppppppppppp")]
    public void Constructor_WhenLastNameLengthIsOutOfArea_ThrowsArgumentException(string lastName)
    {
        // Act
        var act = () => new TestUser("Peter", lastName, "test@test.com");
        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Constructor_WhenEmailIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var email = "";

        // Act
        var act = () => new TestUser("Peter", "Hansen", email);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("@gmail.com")]
    [InlineData("test@gmail")]
    [InlineData("testgmail.com")]
    public void Constructor_WhenEmailIsInvalid_ThrowsArgumentException(string email)
    {
        // Act
        var act = () => new TestUser("Peter", "Hansen", email);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }


}
