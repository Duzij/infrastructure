using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Book : DomainAggregate
    {
        public BookAmount Amount { get; private set; }
        public BookTitle Title { get; private set; }
        public string Description { get; private set; }
        public AuthorId AuthorId { get; private set; }
        public string AuthorName { get; private set; }
        public BookState State { get; private set; }

        public static Book Create(string title, string description, AuthorId authorId, string authorName)
        {
            var book = new Book(
                TypedId.GetNewId<BookId>(), title, description, authorId, authorName);
            book.AddEvent(new BookCreated(book.Id.Value, title, description, authorId.Value.ToString()));
            return book;
        }

        private Book(BookId id, string title, string description, AuthorId authorId, string authorName)
        {
            Id = id;
            Title = new BookTitle(title);
            Description = description;
            AuthorId = authorId;
            AuthorName = authorName;
            State = BookState.InDatabase;
            Amount = new BookAmount(0);
        }

        public void ChangeAuthor(AuthorId authorId, string newAuthorName)
        {
            if (authorId.Value == this.AuthorId.Value)
            {
                throw new ArgumentException("New author id is same as old author id");
            }
            AddEvent(new BookAuthorIdChanged(Id.Value, this.Title.Value, authorId.Value, AuthorId.Value));
            this.AuthorId = authorId;
            this.AuthorName = newAuthorName;
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
                String.IsNullOrWhiteSpace(AuthorName) ||
                String.IsNullOrWhiteSpace(Description))
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
