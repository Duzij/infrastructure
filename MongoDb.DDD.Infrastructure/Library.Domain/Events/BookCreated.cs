namespace Library.Domain
{
    public class BookCreated
    {
        public BookId BookId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public AuthorId AuthorId { get; private set; }

        public BookCreated(BookId bookId, string title, string description, AuthorId author)
        {
            BookId = bookId;
            Title = title;
            Description = description;
            AuthorId = author;
        }

    }
}
