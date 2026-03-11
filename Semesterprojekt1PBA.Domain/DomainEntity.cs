namespace Semesterprojekt1PBA.Domain;
/// <summary>
///  Author: Michael
/// Represents the base type for domain entities with a unique identifier.
/// </summary>
public abstract class DomainEntity
{
    public Guid Id { get; protected set; }
}