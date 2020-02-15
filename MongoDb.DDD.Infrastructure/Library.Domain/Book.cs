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

        private BookState State {get; set;}
   
        public override void Apply(object @event)
        {
            switch (@event) { 
                case BookCreated e: 
                    Id = new BookId(e.Id); 
                    Price = new Price(e.Price);
                    Amount = new BookAmount(e.Amount);
                    Title = e.Title;
                    Description= e.Description;
                    Author= e.Author;
                    State = BookState.InStock; 
                    break;
                case BookPriceChanged e:
                    Price = new Price(e.Price); 
                    break; 
            }
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

    public class BookPriceChanged
    {
        public decimal Price { get; set; }
    }

    public class BookCreated
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
