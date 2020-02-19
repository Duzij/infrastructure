using Infrastructure.Core;
using Infrastructure.MongoDb;
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
        public Price Price { get; private set; }
        public BookAmount Amount { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string Author { get; private set; }

        public BookState State {get; private set;}

        public Book(Guid id, decimal price, int amount, string title, string description, string category, string author)
        {
            Id = id.ToString();
            Price = new Price(price);
            Amount = new BookAmount(amount);
            Title = title;
            Description = description;
            Category = category;
            Author = author;
            State = BookState.InStock;
            AddEvent(new BookCreated(Id));
        }

        public void ChangeTitle(string title)
        {
            Title = title;
            AddEvent(new BookTitleChange(this.Id));
        }

        public void ChangePrice(decimal newPrice)
        {
            this.Price = new Price(newPrice);
            AddEvent(new BookPriceChanged(this.Id));
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

    internal class BookTitleChange 
    {
        public string EntityId { get; set; }

        public BookTitleChange(string bookId)
        {
            this.EntityId = bookId;
        }

    }

    public class BookPriceChanged 
    {
        public string EntityId { get; set; }

        public BookPriceChanged(string bookId)
        {
            this.EntityId = bookId;
        }
    }

    public class BookCreated 
    {
        public string EntityId { get; set; }

        public BookCreated(string bookId)
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
