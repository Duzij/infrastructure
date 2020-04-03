using Infrastructure.Core;
using System.Collections.Generic;

namespace Library.Domain
{
    public class AuthorFullName : ValueObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }

        public AuthorFullName(string authorName)
        {
            var words = authorName.Split(' ');
            Name = words[0];
            Surname = words[1];
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
