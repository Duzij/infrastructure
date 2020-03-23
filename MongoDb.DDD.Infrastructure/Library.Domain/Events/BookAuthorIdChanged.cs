namespace Library.Domain
{
    public class BookAuthorNameChanged
    {
        public string BookId { get; }
        public string BookTitle { get; }
        public string NewAuthorId { get; }
        public string OldAuthorId { get; }

        public BookAuthorNameChanged(string bookId, string bookTitle, string newAuthorId, string oldAuthorId)
        {
            BookId = bookId;
            BookTitle = bookTitle;
            NewAuthorId = newAuthorId;
            OldAuthorId = oldAuthorId;
        }

    }
}
