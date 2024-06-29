using Infrastructure.Core;
using Library.ApplicationLayer.DTO;
using Library.Domain.Events;

namespace Library.ApplicationLayer.EventHandlers
{
    public class BookTitleChangedEventHandler : IEventHandler<BookTitleChanged>
    {
        private readonly IAuthorFacade authorFacade;
        private readonly ILibraryRecordFacade libraryRecordFacade;

        public BookTitleChangedEventHandler(IAuthorFacade authorFacade, ILibraryRecordFacade libraryRecordFacade)
        {
            this.authorFacade = authorFacade;
            this.libraryRecordFacade = libraryRecordFacade;
        }

        public async Task Handle(BookTitleChanged @event)
        {
            List<AuthorDetailDTO> affectedAuthors = await authorFacade.GetAuthorsByBookAsync(@event.OldTitle);
            foreach (var author in affectedAuthors)
            {
                author.BookTitles.Remove(@event.OldTitle);
                author.BookTitles.Add(@event.NewTitle);
                await authorFacade.Update(author);
            }

            await libraryRecordFacade.UpdateLibraryRecordsWithNewBookTitleAsync(@event.BookId, @event.NewTitle);
        }
    }
}
