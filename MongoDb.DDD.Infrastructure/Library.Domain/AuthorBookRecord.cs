using Library.Domain.Id;

namespace Library.Domain
{
    public class AuthorBookRecord
    {
        public BookId BookId { get; }
        public BookTitle Title { get; }

        public AuthorBookRecord(BookId bookId, BookTitle title)
        {
            BookId = bookId;
            Title = title;
        }
    }
}
