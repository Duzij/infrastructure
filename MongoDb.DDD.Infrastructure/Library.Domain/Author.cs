using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Author : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public IList<string> BookTitles { get; set; }

        public Author(Guid id, IList<string> bookTitles, string name, string surname)
        {
            Id = new AuthorId(id);
            BookTitles = bookTitles;
            Name = name;
            Surname = surname;
        }
        public override void CheckState()
        {
            if (Id == null || Id.Value == Guid.Empty || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || BookTitles == null)
            {
                throw new InvalidEntityStateException();
            }
        }
    }

    public class AuthorId : IId<Guid>
    {
        public Guid Value { get; set; }

        public AuthorId(Guid id)
        {
            Value = id;
        }
    }
}
