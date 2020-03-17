using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Author : DomainAggregate
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public IList<BookId> Books { get; private set; }

        public static Author Create(IList<BookId> books, string name, string surname)
        {
            var author = new Author(TypedId.GetNewId<AuthorId>(), books, name,surname);
            return author;
        }

        public static Author Create(string name, string surname)
        {
            var author = new Author(TypedId.GetNewId<AuthorId>(), name, surname);
            return author;
        }

        private Author(AuthorId id, IList<BookId> books, string name, string surname)
        {
            Id = id;
            Books = books;
            Name = name;
            Surname = surname;
        }

        private Author(AuthorId id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
            this.Books = new List<BookId>();
        }


        public override void CheckState()
        {
            if (Id == null || string.IsNullOrWhiteSpace(Id.Value) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || Books == null)
            {
                throw new InvalidEntityStateException();
            }
        }

        public void UpdateBooks(IList<BookId> books)
        {
            this.Books = books;
            CheckState();
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void ChangeSurname(string surname)
        {
            this.Surname = surname;
        }
    }
}
