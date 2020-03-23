using Infrastructure.Core;
using System;
using System.Collections.Generic;

namespace Library.Domain
{
    public class BookTitle : Value
    {
        public string Value;

        public BookTitle(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Book title cannot be null of white space");
            }
            this.Value = title;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new List<object>() { this.Value };
        }
    }
}
