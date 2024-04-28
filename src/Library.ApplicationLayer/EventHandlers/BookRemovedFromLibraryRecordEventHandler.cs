using Infrastructure.Core;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.EventHandlers
{
    public class BookRemovedFromLibraryRecordEventHandler : IEventHandler<BookRemovedFromLibraryRecord>
    {
        private readonly IRepository<Book, string> repository;

        public BookRemovedFromLibraryRecordEventHandler(IRepository<Book, string> repository)
        {
            this.repository = repository;
        }
        public async Task Handle(BookRemovedFromLibraryRecord @event)
        {
            await repository.ModifyAsync(book => book.AddStock(new BookAmount(@event.BookStock)), @event.BookId);
        }
    }
}
