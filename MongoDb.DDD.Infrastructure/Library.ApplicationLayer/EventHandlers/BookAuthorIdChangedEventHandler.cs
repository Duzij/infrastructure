using Infrastructure.Core;
using Library.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Library.ApplicationLayer.EventHandlers
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

            newAuthor.BookTitles ??= [];

            newAuthor.BookTitles.Add(@event.BookTitle);
            await authorFacade.Update(newAuthor);
            logger.LogInformation($"{newAuthor.Name} titles updated. {@event.BookTitle} added.");
        }
    }
}
