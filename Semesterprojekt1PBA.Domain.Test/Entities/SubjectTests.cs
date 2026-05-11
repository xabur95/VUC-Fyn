using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Domain.Test.Entities
{
    public class SubjectTests
    {
        [Fact]
        public void Create_WithValidData_ShouldCreateSubject()
        {
            // Arrange
            var name = "Math";
            var level = Level.A;

            // Act
            var subject = Subject.Create(name, level);

            // Assert
            Assert.NotNull(subject);
            Assert.Equal(name, subject.Name);
            Assert.Equal(level, subject.Level);
            Assert.NotEqual(Guid.Empty, subject.Id);
            Assert.Empty(subject.Topics);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_WithInvalidName_ShouldThrowErrorException(string invalidName)
        {
            // Arrange
            var level = Level.B;

            // Act & Assert
            var ex = Assert.Throws<ErrorException>(() => Subject.Create(invalidName, level));

            Assert.Equal("Subject name cannot be empty.", ex.Message);
            Assert.Equal("INVALID_SUBJECT_NAME", ex.ErrorCode);
        }

        [Fact]
        public void AddTopic_ShouldAddTopicToSubject()
        {
            // Arrange
            var subject = Subject.Create("Biology", Level.C);
            var topic = Topic.Create("Cells");

            // Act
            subject.AddTopic(topic);

            // Assert
            Assert.Single(subject.Topics);
            Assert.Contains(topic, subject.Topics);
        }

        [Fact]
        public void AddTopic_WithDuplicateName_ShouldThrowErrorException()
        {
            // Arrange
            var subject = Subject.Create("Physics", Level.B);
            var topic1 = Topic.Create("Energy");
            var topic2 = Topic.Create("energy"); // same name, different casing

            subject.AddTopic(topic1);

            // Act & Assert
            var ex = Assert.Throws<ErrorException>(() => subject.AddTopic(topic2));

            Assert.Equal("Topic already exists in this subject.", ex.Message);
        }

        [Fact]
        public void AddTopic_ShouldBeCaseInsensitiveForUniqueness()
        {
            // Arrange
            var subject = Subject.Create("Chemistry", Level.A);
            var topic1 = Topic.Create("Atoms");
            var topic2 = Topic.Create("atoms");

            // Act
            subject.AddTopic(topic1);

            // Assert
            Assert.Throws<ErrorException>(() => subject.AddTopic(topic2));
        }

        [Fact]
        public void DeleteTopic_ShouldRemoveTopic()
        {
            // Arrange
            var subject = Subject.Create("History", Level.C);
            var topic = Topic.Create("WW2");

            subject.AddTopic(topic);

            // Act
            subject.DeleteTopic(topic);

            // Assert
            Assert.Empty(subject.Topics);
        }

        [Fact]
        public void DeleteTopic_NonExistingTopic_ShouldDoNothing()
        {
            // Arrange
            var subject = Subject.Create("Geography", Level.B);
            var topic = Topic.Create("Maps");

            // Act
            subject.DeleteTopic(topic);

            // Assert
            Assert.Empty(subject.Topics);
        }
    }
}
