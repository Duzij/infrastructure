using Infrastructure.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Domain
{
    public class BookId : Entity
    {
        private readonly Guid value;

        public static implicit operator Guid(BookId self) => self.value;

        public BookId(Guid value)
        {
            this.value = value;
        }

        public override bool IsValid()
        {
            return value == default;
        }
    }
}
