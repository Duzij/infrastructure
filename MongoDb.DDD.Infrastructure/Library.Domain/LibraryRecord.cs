using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class LibraryRecord : DomainAggregate
    {
        public User User { get; set; }
        public ICollection<string> BookIsbns { get; set; }

        public static LibraryRecord Create(User user)
        {
            var record = new LibraryRecord(TypedId.GetNewId<LibraryRecordId>(), user);
            return record;
        }
        private LibraryRecord(LibraryRecordId id, User user)
        {
            Id = id;
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
