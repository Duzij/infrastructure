namespace Library.Domain
{
    public class BookCreated
    {
        public string BookId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string AuthorId { get; private set; }

        public BookCreated(string bookId, string title, string description, string author)
        {
            BookId = bookId;
            Title = title;
            Description = description;
            AuthorId = author;
        }

    }
}
