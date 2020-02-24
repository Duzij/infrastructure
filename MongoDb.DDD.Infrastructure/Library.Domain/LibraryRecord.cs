using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class LibraryRecord : Entity
    {
        public User User { get; set; }
        public ICollection<string> BookIsbns { get; set; }

        public LibraryRecord(User user)
        {
            if (user.IsNotBanned)
            {
                User = user;
            }
            throw new InvalidEntityStateException();
        }

        public void AddBook(string ISBN)
        {
            BookIsbns.Add(ISBN);
            AddEvent(new BookAddedToLibraryRecord(ISBN, Id.Value));
        }

        public void RemveBook(string ISBN)
        {
            BookIsbns.Remove(ISBN);
            AddEvent(new BookRemovedFromLibraryRecord(ISBN, Id.Value));
        }

        public override void CheckState()
        {
            if (BookIsbns != null &&
                BookIsbns.Count > 0 && 
                User != null && 
                User.IsNotBanned)
            {
                throw new InvalidEntityStateException();
            }
        }
    }

    public class BookRemovedFromLibraryRecord
    {
        private string iSBN;
        private string BookId;

        public BookRemovedFromLibraryRecord(string iSBN, string bookId)
        {
            this.iSBN = iSBN;
            this.BookId = bookId;
        }
    }

    public class BookAddedToLibraryRecord
    {
        private string iSBN;
        private string BookId;

        public BookAddedToLibraryRecord(string iSBN, string bookId)
        {
            this.iSBN = iSBN;
            this.BookId = bookId;
        }
    }
}
