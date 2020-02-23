using Infrastructure.Core;
using Infrastructure.MongoDb;
using Library.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer
{
    public class BookCreatedEventHandler : IEventHandler<BookCreated>
    {
        private readonly ILogger<BookCreatedEventHandler> logger;

        public BookCreatedEventHandler(ILogger<BookCreatedEventHandler> logger)
        {
            this.logger = logger;
        }
        public void Handle(BookCreated @event)
        {
            logger.LogInformation(@event.Description);
            logger.LogInformation(@event.Title);
            logger.LogInformation(@event.Author);
        }
    }
}
