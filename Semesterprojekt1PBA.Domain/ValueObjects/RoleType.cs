namespace Semesterprojekt1PBA.Domain.ValueObjects;
/// <summary>
/// Author: Michael
/// Angiver mulige bruger roller i systemet.
/// Brug denne enum til at skelne mellem typer som student, lærer og Admin ved styring af adgang og logik.
/// </summary>

public enum RoleType
{
    Student,
    Teacher,
    Admin
}