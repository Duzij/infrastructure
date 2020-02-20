using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class BookId : Value<BookId>
    {
        public Guid value { get; set; }

        public static implicit operator Guid(BookId self) => self.value;

        public BookId(Guid value)
        {
            this.value = value;
        }
    }
}
