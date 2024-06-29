using Infrastructure.Core;
using Library.Domain.DomainAggregates;
using Library.Domain.Events;
using Library.Domain.Id;

namespace Library.ApplicationLayer.EventHandlers
{
    public class AuthorUpdatedEventHandler : IEventHandler<AuthorUpdated>
    {
        private readonly IRepository<Book, string> repository;
        private readonly IBookFacade bookFacade;

        public AuthorUpdatedEventHandler(IRepository<Book, string> repository, IBookFacade bookFacade)
        {
            this.repository = repository;
            this.bookFacade = bookFacade;
        }
        public async Task Handle(AuthorUpdated @event)
        {
            List<BookId> bookIds = await bookFacade.GetBookIdsByAuthorFullNameAsync(@event.oldName);

            foreach (var id in bookIds)
            {
                await repository.ModifyAsync(b => b.ChangeAuthor(@event.newName, @event.AuthorId), id);
            }
        }
    }
}
