using Infrastructure.Core;
using Library.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    /// <summary>
    /// In case book title changed all related authors should be notified and updated
    /// </summary>
    public class BookAuthorIdChangedEventHandler : IEventHandler<BookAuthorNameChanged>
    {
        private readonly IAuthorFacade authorFacade;
        private readonly ILogger<BookAuthorIdChangedEventHandler> logger;

        public BookAuthorIdChangedEventHandler(IAuthorFacade authorFacade, ILogger<BookAuthorIdChangedEventHandler> logger)
        {
            this.authorFacade = authorFacade;
            this.logger = logger;
        }
        public async Task Handle(BookAuthorNameChanged @event)
        {
            var oldAuthor = await authorFacade.GetById(@event.OldAuthorId);
            oldAuthor.BookTitles.Remove(@event.BookTitle);
            await authorFacade.Update(oldAuthor);
            logger.LogInformation($"{oldAuthor.Name} titles updated. {@event.BookTitle} removed.");

            var newAuthor = await authorFacade.GetById(@event.NewAuthorId);

            if (newAuthor.BookTitles == null)
            {
                newAuthor.BookTitles = new List<string>();
            }

            newAuthor.BookTitles.Add(@event.BookTitle);
            await authorFacade.Update(newAuthor);
            logger.LogInformation($"{newAuthor.Name} titles updated. {@event.BookTitle} added.");
        }
    }
}
