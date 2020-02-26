using Infrastructure.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoDb
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
