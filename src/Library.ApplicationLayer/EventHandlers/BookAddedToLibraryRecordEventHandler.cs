using Infrastructure.Core;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.EventHandlers
{
    public class BookAddedToLibraryRecordEventHandler : IEventHandler<BookAddedToLibraryRecord>
    {
        private readonly IRepository<Book, string> repository;

        public BookAddedToLibraryRecordEventHandler(IRepository<Book, string> repository)
        {
            this.repository = repository;
        }
        public async Task Handle(BookAddedToLibraryRecord @event)
        {
            await repository.ModifyAsync(book => book.RemoveStock(new BookAmount(@event.BookStock)), @event.BookId);
        }
    }
}
