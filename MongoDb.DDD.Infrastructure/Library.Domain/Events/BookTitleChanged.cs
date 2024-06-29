namespace Library.Domain.Events
{
    public class BookTitleChanged
    {
        public BookTitleChanged(string bookId, string oldTitle, string newTitle)
        {
            BookId = bookId;
            OldTitle = oldTitle;
            NewTitle = newTitle;
        }

        public string BookId { get; }
        public string OldTitle { get; }
        public string NewTitle { get; }
    }
}
