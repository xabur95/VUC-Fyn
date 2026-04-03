using System;
using Xunit;
using Semesterprojekt1PBA.Domain.ValueObjects;

namespace Semesterprojekt1PBA.Domain.Test.ValueObjects
{
    public class TitleTests
    {
        [Fact]
        public void Create_WithValidTitle_ReturnsTitle()
        {
            // Act
            var title = Title.Create("Valid Title", Array.Empty<string>());

            // Assert
            Assert.NotNull(title);
            Assert.Equal("Valid Title", title.Value);
        }

        [Fact]
        public void Create_WithDuplicateTitle_ThrowsArgumentException()
        {
            // Arrange
            var otherTitles = new[] { "DupTitle", "Other" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Title.Create("DupTitle", otherTitles));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WithNullOrWhitespace_ThrowsArgumentException(string value)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Title.Create(value!, Array.Empty<string>()));
        }

        [Fact]
        public void Create_WithTooLong_ThrowsArgumentException()
        {
            // Arrange
            var longString = new string('a', 51);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Title.Create(longString, Array.Empty<string>()));
        }

        [Fact]
        public void ImplicitConversion_ToAndFromString_Works()
        {
            // Arrange
            var title = Title.Create("X", Array.Empty<string>());

            // Act
            string asString = title;
            Title fromString = "Y";

            // Assert
            Assert.Equal("X", asString);
            Assert.Equal("Y", fromString.Value);
        }
    }
}
