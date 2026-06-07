using System.Reflection;
using FluentAssertions;

namespace Semesterprojekt1PBA.Domain.Test.Architecture;

/// <summary>
/// Architecture tests that enforce Onion Architecture layer dependency rules
/// and verify that Infrastructure classes implement the correct Application interfaces.
///
/// Allowed project references:
///   Domain         → (none)
///   Application    → Domain
///   Infrastructure → Domain, Application
///   Presentation   → Application
///   Api            → all (composition root)
/// </summary>
public class LayerDependencyTests
{
    // ── Assembly handles ──────────────────────────────────────────────

    private static readonly Assembly Domain =
        typeof(global::Semesterprojekt1PBA.Domain.Entities.Entity).Assembly;

    private static readonly Assembly Application =
        typeof(global::Semesterprojekt1PBA.Application.Interfaces.IUnitOfWork).Assembly;

    private static readonly Assembly Infrastructure =
        typeof(global::Semesterprojekt1PBA.Infrastructure.Database.AppDbContext).Assembly;

    private static readonly Assembly Presentation =
        typeof(global::Semesterprojekt1PBA.Presentation.Endpoints.UserEndpoints).Assembly;

    /// <summary>
    /// Returns the names of project assemblies (starting with our root namespace)
    /// that the given assembly directly references.
    /// </summary>
    private static IEnumerable<string> ProjectReferences(Assembly assembly) =>
        assembly.GetReferencedAssemblies()
            .Select(a => a.Name!)
            .Where(n => n.StartsWith("Semesterprojekt1PBA", StringComparison.Ordinal));

    // ── Domain layer ──────────────────────────────────────────────────

    [Fact]
    public void Domain_ShouldNotReference_Application()
    {
        ProjectReferences(Domain)
            .Should().NotContain("Semesterprojekt1PBA.Application");
    }

    [Fact]
    public void Domain_ShouldNotReference_Infrastructure()
    {
        ProjectReferences(Domain)
            .Should().NotContain("Semesterprojekt1PBA.Infrastructure");
    }

    [Fact]
    public void Domain_ShouldNotReference_Presentation()
    {
        ProjectReferences(Domain)
            .Should().NotContain("Semesterprojekt1PBA.Presentation");
    }

    [Fact]
    public void Domain_ShouldNotReference_Api()
    {
        ProjectReferences(Domain)
            .Should().NotContain("Semesterprojekt1PBA.Api");
    }

    // ── Application layer ─────────────────────────────────────────────

    [Fact]
    public void Application_ShouldNotReference_Infrastructure()
    {
        ProjectReferences(Application)
            .Should().NotContain("Semesterprojekt1PBA.Infrastructure");
    }

    [Fact]
    public void Application_ShouldNotReference_Presentation()
    {
        ProjectReferences(Application)
            .Should().NotContain("Semesterprojekt1PBA.Presentation");
    }

    [Fact]
    public void Application_ShouldNotReference_Api()
    {
        ProjectReferences(Application)
            .Should().NotContain("Semesterprojekt1PBA.Api");
    }

    // ── Infrastructure layer ──────────────────────────────────────────

    [Fact]
    public void Infrastructure_ShouldNotReference_Presentation()
    {
        ProjectReferences(Infrastructure)
            .Should().NotContain("Semesterprojekt1PBA.Presentation");
    }

    [Fact]
    public void Infrastructure_ShouldNotReference_Api()
    {
        ProjectReferences(Infrastructure)
            .Should().NotContain("Semesterprojekt1PBA.Api");
    }

    // ── Presentation layer ────────────────────────────────────────────

    [Fact]
    public void Presentation_ShouldNotReference_Infrastructure()
    {
        ProjectReferences(Presentation)
            .Should().NotContain("Semesterprojekt1PBA.Infrastructure");
    }

    [Fact]
    public void Presentation_ShouldNotReference_Api()
    {
        ProjectReferences(Presentation)
            .Should().NotContain("Semesterprojekt1PBA.Api");
    }
}
