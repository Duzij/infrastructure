namespace Infrastructure.Core
{
    public interface IQuery<TResult>
    {
        public abstract Task<IList<TResult>> GetResultsAsync();
    }
}
