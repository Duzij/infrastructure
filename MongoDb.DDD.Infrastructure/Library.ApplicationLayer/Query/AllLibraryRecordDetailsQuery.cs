using Infrastructure.Core;
using Infrastructure.MongoDb;
using Library.ApplicationLayer.DTO;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.Query
{
    public class AllLibraryRecordDetailsQuery : Query<LibraryRecordDetailDTO>
    {

        public AllLibraryRecordDetailsQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<LibraryRecordDetailDTO>> GetResultsAsync()
        {
            var records = base.dbContext.GetCollection<LibraryRecord>().AsQueryable();
            var recordDetails = new List<LibraryRecordDetailDTO>();

            foreach (var item in records)
            {
                recordDetails.Add(LibraryRecordConverter.Convert(item));
            }

            return recordDetails;
        }
    }
}
