using FluentAssertions;
using Semesterprojekt1PBA.Application.Interfaces;
using Semesterprojekt1PBA.Application.Interfaces.Repositories;
using Semesterprojekt1PBA.Domain.Entities;
using Semesterprojekt1PBA.Domain.Interfaces;
using Semesterprojekt1PBA.Domain.Policies;
using Semesterprojekt1PBA.Infrastructure.Database;
using Semesterprojekt1PBA.Infrastructure.Database.Repositories;

namespace Semesterprojekt1PBA.Domain.Test.Architecture;

/// <summary>
/// Verifies that concrete classes implement the correct interfaces
/// and that domain entities inherit from the correct base types.
/// </summary>
public class InterfaceImplementationTests
{
    // ── Repository implementations ────────────────────────────────────

    [Fact]
    public void UserRepository_ShouldImplement_IUserRepository()
    {
        typeof(UserRepository).Should().Implement<IUserRepository>();
    }

    [Fact]
    public void SchoolRepository_ShouldImplement_ISchoolRepository()
    {
        typeof(SchoolRepository).Should().Implement<ISchoolRepository>();
    }

    [Fact]
    public void ClassRepository_ShouldImplement_IClassRepository()
    {
        typeof(ClassRepository).Should().Implement<IClassRepository>();
    }

    [Fact]
    public void QuestionRepository_ShouldImplement_IQuestionRepository()
    {
        typeof(QuestionRepository).Should().Implement<IQuestionRepository>();
    }

    [Fact]
    public void TagRepository_ShouldImplement_ITagRepository()
    {
        typeof(TagRepository).Should().Implement<ITagRepository>();
    }

    [Fact]
    public void SubjectRepository_ShouldImplement_ISubjectRepository()
    {
        typeof(SubjectRepository).Should().Implement<ISubjectRepository>();
    }

    [Fact]
    public void TopicRepository_ShouldImplement_ITopicRepository()
    {
        typeof(TopicRepository).Should().Implement<ITopicRepository>();
    }

    [Fact]
    public void UnitOfWork_ShouldImplement_IUnitOfWork()
    {
        typeof(UnitOfWork<AppDbContext>).Should().Implement<IUnitOfWork>();
    }

    // ── Domain entity base types ──────────────────────────────────────

    [Fact]
    public void User_ShouldInheritFrom_Entity()
    {
        typeof(User).Should().BeDerivedFrom<Entity>();
    }

    [Fact]
    public void Student_ShouldInheritFrom_User()
    {
        typeof(Student).Should().BeDerivedFrom<User>();
    }

    [Fact]
    public void Teacher_ShouldInheritFrom_User()
    {
        typeof(Teacher).Should().BeDerivedFrom<User>();
    }

    [Fact]
    public void Admin_ShouldInheritFrom_User()
    {
        typeof(Admin).Should().BeDerivedFrom<User>();
    }

    [Fact]
    public void School_ShouldInheritFrom_Entity()
    {
        typeof(School).Should().BeDerivedFrom<Entity>();
    }

    [Fact]
    public void Class_ShouldInheritFrom_Entity()
    {
        typeof(Class).Should().BeDerivedFrom<Entity>();
    }

    [Fact]
    public void Question_ShouldInheritFrom_Entity()
    {
        typeof(Question).Should().BeDerivedFrom<Entity>();
    }

    [Fact]
    public void Answer_ShouldInheritFrom_Entity()
    {
        typeof(Answer).Should().BeDerivedFrom<Entity>();
    }

    [Fact]
    public void Tag_ShouldInheritFrom_Entity()
    {
        typeof(Tag).Should().BeDerivedFrom<Entity>();
    }

    [Fact]
    public void Subject_ShouldInheritFrom_Entity()
    {
        typeof(Subject).Should().BeDerivedFrom<Entity>();
    }

    [Fact]
    public void Topic_ShouldInheritFrom_Entity()
    {
        typeof(Topic).Should().BeDerivedFrom<Entity>();
    }

    // ── Role policy implementations ───────────────────────────────────

    [Fact]
    public void StudentRolePolicy_ShouldImplement_IRolePolicy()
    {
        typeof(RolePolicies.StudentRolePolicy).Should().Implement<IRolePolicy>();
    }

    [Fact]
    public void TeacherRolePolicy_ShouldImplement_IRolePolicy()
    {
        typeof(RolePolicies.TeacherRolePolicy).Should().Implement<IRolePolicy>();
    }

    [Fact]
    public void AdminRolePolicy_ShouldImplement_IRolePolicy()
    {
        typeof(RolePolicies.AdminRolePolicy).Should().Implement<IRolePolicy>();
    }
}
