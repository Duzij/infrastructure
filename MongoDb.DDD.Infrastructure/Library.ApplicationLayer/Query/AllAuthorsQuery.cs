using Infrastructure.MongoDb;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.Query
{
    public class AllAuthorsQuery : Query<AuthorDetailDTO>
    {
        public AllAuthorsQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<AuthorDetailDTO>> GetResultsAsync()
        {
            var authors = dbContext.GetCollection<Author>().AsQueryable();

            var authorDetails = new List<AuthorDetailDTO>();
            foreach (var author in authors)
            {
                authorDetails.Add(new AuthorDetailDTO(author.Id.Value, author.Name, author.Surname, author.Books));
            }

            return authorDetails;
        }
    }
}
