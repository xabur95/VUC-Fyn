using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt1PBA.Domain;
/// <summary>
/// Author: Michael
/// Abstract base class for entities that require optimistic concurrency control.
/// Derived classes inherit a RowVersion timestamp that is updated by the database
/// on each modification. If two users attempt to update the same entity simultaneously,
/// the first update succeeds and the RowVersion is incremented. The second update is
/// rejected because its RowVersion no longer matches the database, preventing
/// conflicting overwrites.
/// </summary>
public abstract class OptimisticDomainEntity : DomainEntity
{
    [Timestamp] public byte[] RowVersion { get; protected set; } = null;
}