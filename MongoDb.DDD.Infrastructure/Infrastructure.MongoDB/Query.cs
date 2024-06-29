using Infrastructure.Core;

namespace Infrastructure.MongoDB
{
    public abstract class Query<TResult> : IQuery<TResult>
    {
        public readonly IMongoDbContext dbContext;

        public Query(IMongoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public abstract Task<IList<TResult>> GetResultsAsync();
    }
}
