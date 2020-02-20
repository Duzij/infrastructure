using Infrastructure.Core;
using Infrastructure.MongoDb;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer
{
    public class BookCreatedEventHandler : IEventHandler<Event>
    {
        public void Handle(Event @event)
        {
            var createEvent = (BookCreated)@event.EventValue;
            Console.WriteLine(createEvent.Description);
            Console.WriteLine(createEvent.Title);
            Console.WriteLine(createEvent.Author);
        }
    }
}
