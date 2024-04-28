using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
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
            if (this.AuthorName == author)
            {
                throw new InvalidOperationException("Author name was already changed");
            }
            AddEvent(new BookAuthorNameChanged(Id.Value, this.Title.Value, authorId.Value, AuthorId.Value));
            this.AuthorId = authorId;
            this.AuthorName = author;
        }

        public void AddStock(BookAmount amount)
        {
            this.Amount = new BookAmount(this.Amount.Amount + amount.Amount);
            this.State = BookState.InStock;
        }

        public void RemoveStock(BookAmount amount)
        {
            this.Amount = new BookAmount(this.Amount.Amount - amount.Amount);
            if (this.Amount.Amount == 0)
            {
                this.State = BookState.InDatabase;
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
            AddEvent(new BookTitleChanged(Id.Value, this.Title.Value, bookTitle.Value));
            this.Title = bookTitle;
        }

        public void ChangeDescription(string description)
        {
            this.Description = description;
        }
    }
}
