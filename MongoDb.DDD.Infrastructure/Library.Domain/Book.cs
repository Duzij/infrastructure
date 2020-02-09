using Infrastructure.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Domain;

namespace Test.DAL
{
    public class Book : Entity
    {
        public readonly BookId Id;
        public readonly Price price;
        private BookState state {get; set;}

        public Book(BookId id, Price price)
        {
            this.Id = id;
            this.price = price;
            this.state = BookState.Inactive;
        }
        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public string BookName { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }

        
    }
}
