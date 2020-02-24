using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Book : Entity
    {
        public Price Price { get; private set; }
        public BookAmount Amount { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public AuthorId AuthorId { get; private set; }

        public BookState State {get; private set;}

        public Book(Guid id, decimal price, int amount, string title, string description, string category, AuthorId authorId)
        {
            Id = new BookId(id);
            Price = new Price(price);
            Amount = new BookAmount(amount);
            Title = title;
            Description = description;
            Category = category;
            AuthorId = authorId;
            State = BookState.InStock;
            AddEvent(new BookCreated(Id.Value, price, amount, title, description, category, authorId.Value.ToString()));
        }
   
        public override void CheckState()
        {
            if (Id == null || Price == null || Amount == null || AuthorId == null ||
                String.IsNullOrWhiteSpace(Title) ||
                String.IsNullOrWhiteSpace(Category) ||
                String.IsNullOrWhiteSpace(Description))
            {
                throw new InvalidEntityStateException();
            }
        }
    }

    public class BookCreated 
    {
        public Guid BookId { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string AuthorId { get; private set; }
        public string Category { get; private set; }
        public decimal Price { get; private set; }
        public int Amount { get; private set; }

        public BookCreated(Guid bookId, decimal price, int amount, string title, string description, string category, string author)
        {
            BookId = bookId;
            Price = price;
            Amount = amount;
            Title = title;
            Description = description;
            Category = category;
            AuthorId = author;
        }
        
    }
}
