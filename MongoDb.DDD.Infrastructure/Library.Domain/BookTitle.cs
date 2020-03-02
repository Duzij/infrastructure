using System;

namespace Library.Domain
{
    public class BookTitle
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
    }
}
