using Infrastructure.Core;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public class BookTitleChangedEventHandler : IEventHandler<BookTitleChanged>
    {
        private readonly IAuthorFacade authorFacade;

        public BookTitleChangedEventHandler(IAuthorFacade authorFacade)
        {
            this.authorFacade = authorFacade;
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
        }
    }
}
