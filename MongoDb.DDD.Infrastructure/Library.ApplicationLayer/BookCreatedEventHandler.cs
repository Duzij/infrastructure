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
        private readonly IAuthorFacade authorFacade;

        public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger, IAuthorFacade authorFacade)
        {
            this.logger = logger;
            this.authorFacade = authorFacade;
        }
        public async Task Handle(BookCreated @event)
        {
            var author = await authorFacade.GetById(@event.AuthorId);
            if (author.BookTitles == null)
                author.BookTitles = new List<string>();

            author.BookTitles.Add(@event.Title);

            await authorFacade.UpdateAuthorBooksAsync(author.Id, author.BookTitles);
            logger.LogInformation("Author books were updated");
        }
    }
}
