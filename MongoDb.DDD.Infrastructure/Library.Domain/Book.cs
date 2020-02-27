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
            this.Amount = amount;
            this.State = BookState.InStock;
            AddEvent(new BookAddedToStock(amount));
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
    }

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

    public class BookTitle
    {
        public string Value;

        public BookTitle(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Book title cannot be null of white space");
            }
            this.Value = title;
        }
    }

    public class BookAuthorIdChanged
    {
        public string BookId { get; }
        public string BookTitle { get; }
        public string NewAuthorId { get; }
        public string OldAuthorId { get; }

        public BookAuthorIdChanged(string bookId, string bookTitle, string newAuthorId, string oldAuthorId)
        {
            BookId = bookId;
            BookTitle = bookTitle;
            NewAuthorId = newAuthorId;
            OldAuthorId = oldAuthorId;
        }

    }

    public class BookAddedToStock
    {
        public int booksAdded;

        public BookAddedToStock(BookAmount amount)
        {
            this.booksAdded = amount.Amount;
        }
    }

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
