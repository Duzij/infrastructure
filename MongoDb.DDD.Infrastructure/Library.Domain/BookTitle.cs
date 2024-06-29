using Infrastructure.Core;

namespace Library.Domain
{
    public class BookTitle : ValueObject
    {
        public string Value;

        public BookTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Book title cannot be null of white space");
            }
            Value = title;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }
    }
}
