namespace Infrastructure.Core
{
    public interface IId<TKey>
    {
        public TKey Value { get; set; }
    }
}