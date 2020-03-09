using Infrastructure.Core;
using Infrastructure.MongoDb;
using Library.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public class BookCreatedEventHandler : IEventHandler<BookCreated>
    {
        private readonly ILogger<BookCreatedEventHandler> logger;
        private readonly IRepository<Author, string> authorRepository;

        public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger, IRepository<Author, string> authorRepository)
        {
            this.logger = logger;
            this.authorRepository = authorRepository;
        }
        
        public async Task Handle(BookCreated @event)
        {
            await authorRepository.ReplaceAsync(author => {
                var bookList = author.Books;
                bookList.Add(new BookId(@event.BookId));
                author.UpdateBooks(bookList);
            }, @event.AuthorId);
            logger.LogInformation("Author books were updated");
        }
    }
}
