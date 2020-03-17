namespace Library.Domain
{
    public class BookRecord
    {
        public BookId BookId { get; }
        public BookAmount BookAmount { get; }
        public BookTitle Title { get; }

        public BookRecord(BookId bookId, BookAmount bookAmount, BookTitle title)
        {
            BookId = bookId;
            BookAmount = bookAmount;
            Title = title;
        }
    }
}
