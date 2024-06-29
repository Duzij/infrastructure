using Infrastructure.Core;

namespace Library.Domain
{
    public class AuthorFullName : ValueObject
    {
        public string Name { get; }
        public string Surname { get; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }

        public AuthorFullName(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new InvalidEntityStateException("Author name cannot be null or empty");
            }

            var words = authorName.Split(' ');
            try
            {
                Name = words[0];
                Surname = words[1];
            }
            catch
            {
                Name = authorName;
            }
        }

        public AuthorFullName(string name, string surname)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname))
            {
                throw new InvalidEntityStateException("Author name cannot be null or empty");
            }

            Name = name;
            Surname = surname;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Name, Surname];
        }
    }
}
