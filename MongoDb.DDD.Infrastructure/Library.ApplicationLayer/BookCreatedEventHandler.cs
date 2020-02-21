using Infrastructure.Core;
using Infrastructure.MongoDb;
using Library.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer
{
    public class BookCreatedEventHandler : IEventHandler<Event>
    {
        private readonly ILogger<BookCreatedEventHandler> logger;

        public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger)
        {
            this.logger = logger;
        }
        public void Handle(Event @event)
        {
            var createEvent = (BookCreated)@event.EventValue;
            logger.LogInformation(createEvent.Description);
            logger.LogInformation(createEvent.Title);
            logger.LogInformation(createEvent.Author);
        }
    }
}
