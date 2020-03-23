using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Author : AppDomainAggregate
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }

        public IList<AuthorBookRecord> Books { get; private set; }

        public static Author Create(IList<AuthorBookRecord> books, string name, string surname)
        {
            var author = new Author(TypedId.GetNewId<AuthorId>(), books, name,surname);
            return author;
        }

        public static Author Create(string name, string surname)
        {
            var author = new Author(TypedId.GetNewId<AuthorId>(), name, surname);
            return author;
        }

        private Author(AuthorId id, IList<AuthorBookRecord> books, string name, string surname)
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
            this.Books = new List<AuthorBookRecord>();
        }


        public override void CheckState()
        {
            if (Id == null || string.IsNullOrWhiteSpace(Id.Value) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Surname) || Books == null)
            {
                throw new InvalidEntityStateException();
            }
        }

        public void UpdateBooks(IList<AuthorBookRecord> books)
        {
            this.Books = books;
            CheckState();
        }

        public void UpdateAuthor(string name, string surname)
        {
            var oldName = new AuthorFullName(this.Name, this.Surname);
            this.Name = name;
            this.Surname = surname;
            var newName = new AuthorFullName(this.Name, this.Surname);
            AddEvent(new AuthorUpdated(oldName, newName, (AuthorId)Id));
        }
    }
}
