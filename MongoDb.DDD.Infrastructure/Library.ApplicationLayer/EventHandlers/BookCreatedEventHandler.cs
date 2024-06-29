using Infrastructure.Core;
using Library.Domain;
using Library.Domain.DomainAggregates;
using Library.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Library.ApplicationLayer.EventHandlers
{
    public class BookCreatedEventHandler : IEventHandler<BookCreated>
    {
        private readonly ILogger<BookCreatedEventHandler> logger;
        private readonly IRepository<Author, string> authorRepository;
        private readonly IRepository<Book, string> bookRepository;

        public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger, IRepository<Author, string> authorRepository, IRepository<Book, string> bookRepository)
        {
            this.logger = logger;
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
        }

        public async Task Handle(BookCreated @event)
        {
            var book = await bookRepository.GetByIdAsync(@event.BookId);

            await authorRepository.ModifyAsync(author =>
            {
                var bookList = author.Books;
                bookList.Add(new AuthorBookRecord(@event.BookId, book.Title));
                author.UpdateBooks(bookList);
            }, @event.AuthorId);
            logger.LogInformation($"Books by author with id {@event.AuthorId} were updated");
        }
    }
}
