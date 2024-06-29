using Library.Domain.Events;
using Library.Domain.Id;

namespace Library.Domain.DomainAggregates
{
    public class Book : AppAggregate
    {
        public BookAmount Amount { get; private set; }
        public BookTitle Title { get; private set; }
        public string Description { get; private set; }
        public AuthorId AuthorId { get; private set; }
        public AuthorFullName AuthorName { get; private set; }
        public BookState State { get; private set; }

        public static Book Create(string title, string description, AuthorId authorId, AuthorFullName authorName)
        {
            var book = new Book(
                TypedId.GetNewId<BookId>(), title, description, authorId, authorName);
            book.AddEvent(new BookCreated((BookId)book.Id, title, description, authorId));
            return book;
        }

        private Book(BookId id, string title, string description, AuthorId authorId, AuthorFullName authorName)
        {
            Id = id;
            Title = new BookTitle(title);
            Description = description;
            AuthorId = authorId;
            AuthorName = authorName;
            State = BookState.InDatabase;
            Amount = new BookAmount(0);
        }

        public void ChangeAuthor(AuthorFullName author, AuthorId authorId)
        {
            if (AuthorName == author)
            {
                throw new InvalidOperationException("Author name was already changed");
            }
            AddEvent(new BookAuthorNameChanged(Id.Value, Title.Value, authorId.Value, AuthorId.Value));
            AuthorId = authorId;
            AuthorName = author;
        }

        public void AddStock(BookAmount amount)
        {
            Amount = new BookAmount(Amount.Amount + amount.Amount);
            State = BookState.InStock;
        }

        public void RemoveStock(BookAmount amount)
        {
            Amount = new BookAmount(Amount.Amount - amount.Amount);
            if (Amount.Amount == 0)
            {
                State = BookState.InDatabase;
            }
        }

        public override void CheckState()
        {
            if (Id == null || Amount == null ||
                AuthorId == null ||
                Title == null ||
                AuthorName == null)
            {
                throw new InvalidEntityStateException();
            }
        }

        public void ChangeTitle(BookTitle bookTitle)
        {
            AddEvent(new BookTitleChanged(Id.Value, Title.Value, bookTitle.Value));
            Title = bookTitle;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
        }
    }
}
