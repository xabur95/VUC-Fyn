using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Helpers;
using Semesterprojekt1PBA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using Xunit;

namespace Semesterprojekt1PBA.Domain.Test.Entities
{
    public class ClassTests
    {
        [Fact]
        public void Create_WithValidData_ReturnsClass()
        {
            // Arrange
            var title = "Test Class";
            var start = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            var end = DateOnly.FromDateTime(DateTime.Now.AddDays(3));

            // Act
            var cls = Class.Create(title, start, end, Array.Empty<Class>());

            // Assert
            Assert.NotNull(cls);
            Assert.Equal(title, cls.Title.Value);
            Assert.Equal(start, cls.ClassDateRange.Start);
            Assert.Equal(end, cls.ClassDateRange.End);
        }

        [Fact]
        public void Create_WithStartInPast_ThrowsErrorException()
        {
            // Arrange
            var title = "Past Class";
            var start = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            var end = DateOnly.FromDateTime(DateTime.Now.AddDays(2));

            // Act & Assert
            Assert.Throws<ErrorException>(() => Class.Create(title, start, end, Array.Empty<Class>()));
        }

        [Fact]
        public void Create_WithStartEqualToEnd_ThrowsErrorException()
        {
            // Arrange
            var title = "SameDate";
            var start = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
            var end = start;

            // Act & Assert
            Assert.Throws<ErrorException>(() => Class.Create(title, start, end, Array.Empty<Class>()));
        }

        //TODO: when subject is Implemented fully implement this test

        /*    [Fact]
            public void AddSubject_AddsSubject()
            {
                // Arrange
                var cls = Class.Create("S1", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Array.Empty<Class>());
                var subject = Subject.Create("Math", Level.A);

                // Act
                cls.AddSubject(subject);

                // Assert
                Assert.Contains(subject, cls.Subjects);
                Assert.Single(cls.Subjects);
            }

        [Fact]
        public void AddSubject_Duplicate_ThrowsArgumentException()
        {
            // Arrange
            var cls = Class.Create("S2", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Array.Empty<Class>());
            var subject = Subject.Create("History", Level.B);

            // Act
            cls.AddSubject(subject);

            // Assert duplicate throws
            Assert.Throws<ErrorException>(() => cls.AddSubject(Subject.Create("History", Level.B)));
        }

        */

        [Fact]
        public void AddStudent_WithStudentRole_AddsStudent()
        {
            // Arrange
            var cls = Class.Create("S3", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Array.Empty<Class>());
            var student = User.Create("Stu", "Dent", "stu@example.com", RoleType.Student);

            // Act
            cls.AddStudent(student);

            // Assert
            Assert.Contains(student, cls.Students);
            Assert.Single(cls.Students);
        }

        [Fact]
        public void AddStudent_WithWrongRole_ThrowsErrorException()
        {
            // Arrange
            var cls = Class.Create("S4", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Array.Empty<Class>());
            var teacherUser = User.Create("Teach", "Er", "t@example.com", RoleType.Teacher);

            // Act & Assert
            Assert.Throws<ErrorException>(() => cls.AddStudent(teacherUser));
        }

        [Fact]
        public void AddStudent_Duplicate_ThrowsErrorException()
        {
            // Arrange
            var cls = Class.Create("S5", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Array.Empty<Class>());
            var student = User.Create("Dup", "User", "dup@example.com", RoleType.Student);
            cls.AddStudent(student);

            // Act & Assert
            Assert.Throws<ErrorException>(() => cls.AddStudent(student));
        }

        [Fact]
        public void AddTeacher_WithTeacherRole_AddsTeacher()
        {
            // Arrange
            var cls = Class.Create("S6", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Array.Empty<Class>());
            var teacher = User.Create("Bo", "Teacher", "teach@example.com", RoleType.Teacher);

            // Act
            cls.AddTeacher(teacher);

            // Assert
            Assert.Contains(teacher, cls.Teachers);
            Assert.Single(cls.Teachers);
        }

        [Fact]
        public void AddTeacher_WithWrongRole_ThrowsErrorException()
        {
            // Arrange
            var cls = Class.Create("S7", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Array.Empty<Class>());
            var student = User.Create("Not", "Teacher", "nont@example.com", RoleType.Student);

            // Act & Assert
            Assert.Throws<ErrorException>(() => cls.AddTeacher(student));
        }

        [Fact]
        public void AddTeacher_Duplicate_ThrowsErrorException()
        {
            // Arrange
            var cls = Class.Create("S8", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Array.Empty<Class>());
            var teacher = User.Create("DupT", "Eacher", "dupt@example.com", RoleType.Teacher);
            cls.AddTeacher(teacher);

            // Act & Assert
            Assert.Throws<ErrorException>(() => cls.AddTeacher(teacher));
        }
    }
}
