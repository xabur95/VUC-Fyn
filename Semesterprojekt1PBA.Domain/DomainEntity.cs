using System.ComponentModel.DataAnnotations;

namespace Semesterprojekt1PBA.Domain;
/// <summary>
/// Author: Michael
/// Basistype for domain entities med unik identifier.
/// </summary>
public abstract class DomainEntity : IEquatable<DomainEntity>
{
    public Guid Id { get; protected set; }
    [Timestamp] public byte[] RowVersion { get; protected set; }

    public bool Equals(DomainEntity? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((DomainEntity)obj);
    }

    public static bool operator ==(DomainEntity left, DomainEntity right)
    {
        if (left is null)
        {
            return right is null;
        }
        return left.Equals(right);
    }

    public static bool operator !=(DomainEntity left, DomainEntity right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}