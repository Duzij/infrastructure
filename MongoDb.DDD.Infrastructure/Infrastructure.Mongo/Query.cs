using Infrastructure.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDb
{
    public abstract class Query<TEntity, TResult> : IQuery<TEntity, TResult>
    {
        private readonly IMongoDbContext dbContext;

        public Query(IMongoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IList<TResult> GetResults()
        {
            return Process(dbContext.Database.GetCollection<TEntity>(typeof(TEntity).FullName));
        }

        public abstract IList<TResult> Process(IMongoCollection<TEntity> mongoCollection);
    }
}
