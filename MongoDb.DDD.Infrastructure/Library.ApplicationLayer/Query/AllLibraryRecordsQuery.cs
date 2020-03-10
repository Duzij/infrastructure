using Infrastructure.MongoDb;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.Query
{
    public class AllLibraryRecordsQuery : Query<LibraryRecord>
    {

        public AllLibraryRecordsQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<LibraryRecord>> GetResultsAsync()
        {
            var records = base.dbContext.GetCollection<LibraryRecord>().AsQueryable();
            return await records.ToListAsync();
        }
    }
}
