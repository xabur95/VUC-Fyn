using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt1PBA.Domain.Entities;

/// <summary>
///     Author: Michael
///     Basistype for domain entities med unik identifier.
/// </summary>
public abstract class Entity : IEquatable<Entity>
{
    public Guid Id { get; protected set; }
    [Timestamp] public byte[] RowVersion { get; protected set; }

    public bool Equals(Entity? other)
    {
        if (other is null) return false;

        if (ReferenceEquals(this, other)) return true;

        return Id == other.Id;
    }

    //TODO: Undersøg om Equals på Entity er nødvendigt,
    // da det kan være problematisk at sammenligne to forskellige instanser af samme type,
    // selvom de har samme Id.
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != GetType()) return false;

        return Equals((Entity)obj);
    }

    public static bool operator ==(Entity left, Entity right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
