using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Author : DomainAggregate
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public IList<string> BookTitles { get; private set; }

        public static Author Create(IList<string> bookTitles, string name, string surname)
        {
            var author = new Author(TypedId.GetNewId<AuthorId>(),bookTitles,name,surname);
            return author;
        }

        public static Author Create(string name, string surname)
        {
            var author = new Author(TypedId.GetNewId<AuthorId>(), name, surname);
            return author;
        }

        private Author(AuthorId id, IList<string> bookTitles, string name, string surname)
        {
            Id = id;
            BookTitles = bookTitles;
            Name = name;
            Surname = surname;
        }

        private Author(AuthorId id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }


        public override void CheckState()
        {
            if (Id == null || string.IsNullOrWhiteSpace(Id.Value) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || BookTitles == null)
            {
                throw new InvalidEntityStateException();
            }
        }

        public void UpdateBooks(IList<string> bookTitles)
        {
            this.BookTitles = bookTitles;
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
