using Infrastructure.MongoDB;
using Library.ApplicationLayer.DTO;
using Library.Domain.DomainAggregates;
using MongoDB.Driver;

namespace Library.ApplicationLayer.Query
{
    public class AllBookDetailsQuery : Query<BookDetailDTO>
    {
        public AllBookDetailsQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<BookDetailDTO>> GetResultsAsync()
        {
            var bookCollection = dbContext.GetCollection<Book>().AsQueryable();

            var returnCollection = new List<BookDetailDTO>();
            foreach (var book in bookCollection)
            {
                returnCollection.Add(new BookDetailDTO() { Id = book.Id.Value, Title = book.Title.Value, AuthorName = book.AuthorName.ToString(), Amount = book.Amount.Amount, AuthorId = book.AuthorId.Value, Description = book.Description });
            }

            return returnCollection;
        }
    }
}
