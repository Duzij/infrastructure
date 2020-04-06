﻿using Infrastructure.Core;
using Infrastructure.MongoDB;
using Library.ApplicationLayer.DTO;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.Query
{
    public class ValidLibraryRecordDetailsQuery : Query<LibraryRecordDetailDTO>
    {

        public ValidLibraryRecordDetailsQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<LibraryRecordDetailDTO>> GetResultsAsync()
        {
            var records = base.dbContext.GetCollection<LibraryRecord>()
                .AsQueryable()
                .Where(a => !a.IsClosed)
                .OrderByDescending(a => a.CreatedDate);

            var recordDetails = new List<LibraryRecordDetailDTO>();

            foreach (var item in records)
            {
                recordDetails.Add(LibraryRecordMapper.MapTo(item));
            }

            return recordDetails;
        }
    }
}
