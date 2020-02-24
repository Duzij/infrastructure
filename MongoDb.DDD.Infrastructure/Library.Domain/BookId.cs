using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class BookId : IId<string>
    {
        public string Value { get; set; }

        public BookId(string value)
        {
            Value = value;
        }
    }
}
