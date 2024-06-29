using Infrastructure.MongoDB;
using Library.Domain.DomainAggregates;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Library.ApplicationLayer.Query
{
    public class AllBooksQuery : Query<Book>
    {
        public Expression<Func<Book, bool>> Filter { get; set; }
        public AllBooksQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IList<Book>> GetResultsAsync()
        {
            var records = base.dbContext.GetCollection<Book>().AsQueryable();

            if (Filter != null)
            {
                records = records.Where(Filter);
            }

            return await records.ToListAsync();

        }
    }
}
