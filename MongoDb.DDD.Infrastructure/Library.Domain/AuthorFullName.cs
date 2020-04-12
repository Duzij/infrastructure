using Infrastructure.Core;
using System.Collections.Generic;

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
            Name = authorName;
        }

        public AuthorFullName(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new List<object>() { this.Name, this.Surname };
        }
    }
}
