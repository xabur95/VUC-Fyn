using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Domain.Test.Entities
{
    public class TopicTests
    {
        [Fact]
        public void Create_WithValidName_ShouldCreateTopic()
        {
            // Arrange
            var validName = "Algebra";

            // Act
            var topic = Topic.Create(validName);

            // Assert
            Assert.NotNull(topic);
            Assert.Equal(validName, topic.Name);
            Assert.NotEqual(Guid.Empty, topic.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_WithInvalidName_ShouldThrowErrorException(string invalidName)
        {
            // Act & Assert
            var exception = Assert.Throws<ErrorException>(() => Topic.Create(invalidName));

            Assert.Equal("Topic name cannot be empty.", exception.Message);
            Assert.Equal("INVALID_TOPIC_NAME", exception.ErrorCode);
        }
    }
}
