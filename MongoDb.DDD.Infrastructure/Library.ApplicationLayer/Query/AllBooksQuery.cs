using Infrastructure.MongoDb;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.Query
{
    public class AllBooksQuery : Query<BookDetailDTO>
    {
        public AllBooksQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<BookDetailDTO>> GetResultsAsync()
        {
            var bookCollection = dbContext.GetCollection<Book>().AsQueryable();

            var returnCollection = new List<BookDetailDTO>();
            foreach (var book in bookCollection)
            {
                returnCollection.Add(new BookDetailDTO() { Id = book.Id.Value, Title = book.Title.Value, AuthorName = book.AuthorName, Amount = book.Amount.Amount, AuthorId = book.AuthorId.Value, Description = book.Description });
            }

            return returnCollection;
        }
    }
}
