using Infrastructure.MongoDB;
using Library.Domain.DomainAggregates;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Library.ApplicationLayer.Query
{
    public class AllLibraryRecordsQuery : Query<LibraryRecord>
    {
        public Expression<Func<LibraryRecord, bool>> Filter { get; set; }

        public AllLibraryRecordsQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<LibraryRecord>> GetResultsAsync()
        {
            var records = base.dbContext.GetCollection<LibraryRecord>().AsQueryable();

            if (Filter != null)
            {
                records = records.Where(Filter);
            }

            return await records.ToListAsync();
        }
    }
}
