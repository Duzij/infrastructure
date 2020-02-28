using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class LibraryRecord : DomainAggregate
    {
        public User User { get; set; }
        public IList<BookId> BookIds { get; private set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime ReturnDate => CreatedDate.AddDays(7);

        public ReturnFine ReturnFine { get; set; }

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
            else
            {
                throw new InvalidEntityStateException();
            }
            this.CreatedDate = DateTime.UtcNow;
            this.ReturnFine = new ReturnFine(0);
        }

        public void AddBook(BookId bookId)
        {
            BookIds.Add(bookId);
            AddEvent(new BookAddedToLibraryRecord(bookId.Value, Id.Value));
        }

        public void ReturnBook(BookId bookId)
        {
            if (ReturnDate > DateTime.UtcNow)
            {
                BookIds.Remove(bookId);
                AddEvent(new BookRemovedFromLibraryRecord(bookId.Value, Id.Value));
            }
            else
            {
                var oldFineValue = ReturnFine.Value;
                var daysBetweenTodayAndReturnDate = ((TimeSpan)(DateTime.UtcNow - ReturnDate)).Days;
                var newFineValue = daysBetweenTodayAndReturnDate * 10;
                ReturnFine = new ReturnFine(oldFineValue + newFineValue);
            }
        }

        public override void CheckState()
        {
            if (BookIds != null &&
                BookIds.Count > 0 &&
                User != null &&
                User.IsNotBanned)
            {
                throw new InvalidEntityStateException();
            }
        }
    }

    public class BookRemovedFromLibraryRecord
    {
        public string LibraryRecordId { get; set; }
        public string BookId { get; set; }

        public BookRemovedFromLibraryRecord(string bookId, string libraryRecordId)
        {
            this.LibraryRecordId = libraryRecordId;
            this.BookId = bookId;
        }
    }

    public class BookAddedToLibraryRecord
    {
        public string LibraryRecordId { get; set; }
        public string BookId { get; set; }

        public BookAddedToLibraryRecord(string bookId, string libraryRecordId)
        {
            this.LibraryRecordId = libraryRecordId;
            this.BookId = bookId;
        }
    }
}
