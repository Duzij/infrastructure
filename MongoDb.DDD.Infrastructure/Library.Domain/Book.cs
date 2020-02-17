using Infrastructure.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Domain;

namespace Library.Domain
{
    public class Book : Entity
    {
        public BookId Id { get; private set; }
        public Price Price { get; private set; }
        public BookAmount Amount { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string Author { get; private set; }

        public BookState State {get; private set;}

        public Book(Guid id, decimal price, int amount, string title, string description, string category, string author)
        {
            Id = new BookId(id);
            Price = new Price(price);
            Amount = new BookAmount(amount);
            Title = title;
            Description = description;
            Category = category;
            Author = author;
            State = BookState.InStock;
            AddEvent(new BookCreated(Id.value));
        }

        public void ChangeTitle(string title)
        {
            Title = title;
            AddEvent(new BookTitleChange(this.Id.value));
        }

        public void ChangePrice(decimal newPrice)
        {
            this.Price = new Price(newPrice);
            AddEvent(new BookPriceChanged(this.Id.value));
        }
   
        public override bool CheckState()
        {
            if (Id == null || Price == null || Amount == null || 
                String.IsNullOrWhiteSpace(Title) ||
                String.IsNullOrWhiteSpace(Category) ||
                String.IsNullOrWhiteSpace(Author) ||
                String.IsNullOrWhiteSpace(Description))
            {
                throw new InvalidEntityStateException();
            }
            return true;
        }

        
    }

    internal class BookTitleChange : IEvent
    {
        public BookTitleChange(Guid bookId)
        {
            this.EntityId = bookId;
        }

        public Guid EntityId { get; set; }
    }

    public class BookPriceChanged : IEvent
    {
        public Guid EntityId { get; set; }

        public BookPriceChanged(Guid bookId)
        {
            this.EntityId = bookId;
        }
    }

    public class BookCreated : IEvent
    {
        public Guid EntityId { get; set; }

        public BookCreated(Guid bookId)
        {
            this.EntityId = bookId;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
