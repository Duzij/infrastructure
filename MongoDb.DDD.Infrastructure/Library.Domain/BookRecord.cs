using Library.Domain.Id;

namespace Library.Domain
{
    public class BookRecord
    {
        public BookId BookId;
        public BookAmount BookAmount;
        public BookTitle Title;

        public BookRecord(BookId bookId, BookAmount bookAmount, BookTitle title)
        {
            BookId = bookId;
            BookAmount = bookAmount;
            Title = title;
        }
    }
}
