using Infrastructure.MongoDB;
using Library.ApplicationLayer.DTO;
using Library.ApplicationLayer.Mappers;
using Library.Domain.DomainAggregates;
using Library.Domain.Id;
using MongoDB.Driver;

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
                var bookTitles = dbContext.GetCollection<Book>().AsQueryable()
                    .Where(a => a.AuthorId == (AuthorId)author.Id)
                    .Select(b => b.Title.Value)
                    .ToList();

                authorDetails.Add(AuthorMapper.MapTo(author));
            }

            return authorDetails;
        }
    }
}
