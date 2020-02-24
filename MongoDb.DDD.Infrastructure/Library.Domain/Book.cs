using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Book : Entity
    {
        public BookAmount Amount { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public AuthorId AuthorId { get; private set; }
        public string AuthorName { get; private set; }
        public BookState State {get; private set;}

        public Book(Guid id, string title, string description, AuthorId authorId, string authorName)
        {
            Id = new BookId(id.ToString());
            Title = title;
            Description = description;
            AuthorId = authorId;
            AuthorName = authorName;
            State = BookState.InDatabase;
            Amount = new BookAmount(0);
            AddEvent(new BookCreated(Id.Value, title, description, authorId.Value.ToString()));
        }

        public void AddStock(BookAmount amount)
        {
            this.Amount = amount;
            AddEvent(new BookAddedToStock(amount));
        }
   
        public override void CheckState()
        {
            if (Id == null || Amount == null || AuthorId == null ||
                String.IsNullOrWhiteSpace(Title) ||
                String.IsNullOrWhiteSpace(Description))
            {
                throw new InvalidEntityStateException();
            }
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
