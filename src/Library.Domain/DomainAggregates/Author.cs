using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Author : AppAggregate
    {
        public AuthorFullName authorFullName { get; private set; }

        public IList<AuthorBookRecord> Books { get; private set; }

        public static Author Create(AuthorFullName authorFullName)
        {
            var author = new Author(TypedId.GetNewId<AuthorId>(), authorFullName);
            return author;
        }

        private Author(AuthorId id, AuthorFullName authorFullName)
        {
            Id = id;
            this.Books = new List<AuthorBookRecord>();
            this.authorFullName = authorFullName;
        }


        public override void CheckState()
        {
            if (authorFullName == null || Books == null)
            {
                throw new InvalidEntityStateException();
            }
        }

        public void UpdateBooks(IList<AuthorBookRecord> books)
        {
            this.Books = books;
            CheckState();
        }

        public void UpdateAuthorFullName(AuthorFullName newAuthorFullName)
        {
            var oldAuthorName = authorFullName;
            if (authorFullName != newAuthorFullName)
            {
                authorFullName = newAuthorFullName;
                AddEvent(new AuthorUpdated(oldAuthorName, newAuthorFullName, (AuthorId)Id));
            }
        }
    }
}
