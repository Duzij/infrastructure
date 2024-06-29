using Infrastructure.Core;
using Infrastructure.MongoDB;
using Library.ApplicationLayer.DTO;
using Library.ApplicationLayer.Mappers;
using Library.Domain.DomainAggregates;
using Library.Domain.Id;
using MongoDB.Driver;

namespace Library.ApplicationLayer.Query
{
    public class AuthorByBookTitleQuery : Query<AuthorDetailDTO>
    {
        private readonly IRepository<Book, string> bookRepository;

        public AuthorByBookTitleQuery(IMongoDbContext dbContext, IRepository<Book, string> bookRepository) : base(dbContext)
        {
            this.bookRepository = bookRepository;
        }

        public BookId BookId { get; set; }

        public override async Task<IList<AuthorDetailDTO>> GetResultsAsync()
        {
            var authorCollection = dbContext.GetCollection<Author>();

            var authors = await authorCollection.FindAsync(a => a.Books.Any(bId => bId.BookId == BookId));
            var authorDetails = new List<AuthorDetailDTO>();
            foreach (var author in authors.ToEnumerable())
            {
                var books = new List<string>();

                foreach (var bookRecord in author.Books)
                {
                    var book = await bookRepository.GetByIdAsync(bookRecord.BookId);
                    books.Add(book.Title.Value);
                }

                authorDetails.Add(AuthorMapper.MapTo(author));
            }
            return authorDetails;
        }

    }
}
