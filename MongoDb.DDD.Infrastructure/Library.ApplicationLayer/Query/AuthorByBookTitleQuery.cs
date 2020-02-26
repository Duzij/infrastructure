﻿using Infrastructure.MongoDb;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.Query
{
    public class AuthorByBookTitleQuery : Query<AuthorDetailDTO>
    {
        public AuthorByBookTitleQuery(IMongoDbContext dbContext) :  base(dbContext)
        {
        }

        public string BookTitle { get; set; }

        public override async Task<IList<AuthorDetailDTO>> GetResultsAsync()
        {
            var mongoCollection = dbContext.GetCollection<Author>();
            var authors = await mongoCollection.FindAsync(a => a.BookTitles.Contains(BookTitle));
            var authorDetails = new List<AuthorDetailDTO>();
            foreach (var author in authors.ToEnumerable())
            {
                authorDetails.Add(new AuthorDetailDTO(author.Id.Value, author.Name, author.Surname, author.BookTitles));
            }
            return authorDetails;
        }

    }
}
