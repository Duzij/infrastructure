namespace Infrastructure.Core
{

    public interface IEntity<TKey>
    {
        public IId<TKey> Id { get; }

    }
}