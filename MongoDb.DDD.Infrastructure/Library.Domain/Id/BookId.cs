using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class BookId : EntityId<Book>
    {
        public BookId(string value) : base(value)
        {
        }
    }
}
