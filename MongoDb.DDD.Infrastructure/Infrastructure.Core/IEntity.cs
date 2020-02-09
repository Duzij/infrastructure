namespace Infrastructure.Core
{
    public interface IEntity
    {
        bool Equals(object other);
        int GetHashCode();
        bool IsValid();
    }
}