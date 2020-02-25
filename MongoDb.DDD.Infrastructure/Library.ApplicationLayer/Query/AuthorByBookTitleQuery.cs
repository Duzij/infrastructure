using Infrastructure.MongoDb;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer.Query
{
    public class AuthorByBookTitleQuery : Query<Author, AuthorDetailDTO>, IAuthorByBookTitleQuery
    {
        public AuthorByBookTitleQuery(IMongoDbContext dbContext) :  base(dbContext)
        {
        }

        public string BookTitle { get; set; }
        public override IList<AuthorDetailDTO> Process(IMongoCollection<Author> mongoCollection)
        {
            var authors = mongoCollection.Find(a => a.BookTitles.Contains(BookTitle)).ToList();
            var authorDetails = new List<AuthorDetailDTO>();
            foreach (var author in authors)
            {
                authorDetails.Add(new AuthorDetailDTO(author.Id.Value, author.Name, author.Surname, author.BookTitles));
            }
            return authorDetails;
        }

    }
}
