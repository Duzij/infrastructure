using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Domain
{
    public class LibraryRecord : AppAggregate
    {
        public bool IsClosed { get; private set; }
        public UserRecord User { get; private set; }
        public List<BookRecord> Books { get; private set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime ReturnDate => CreatedDate.AddDays(7);

        public ReturnFine ReturnFine { get; private set; }

        public static LibraryRecord Create(UserRecord user, List<BookRecord> books)
        {
            var record = new LibraryRecord(TypedId.GetNewId<LibraryRecordId>(), user, books);
            return record;
        }
        private LibraryRecord(LibraryRecordId id, UserRecord user, List<BookRecord> books)
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
            Books = books;
            foreach (var book in books)
            {
                AddEvent(new BookAddedToLibraryRecord(book.BookId, id, book.BookAmount.Amount));
            }
        }

        public void UpdateUser(UserRecord userRecord)
        {
            if (userRecord.IsNotBanned)
            {
                this.User = userRecord;
            }
        }

        public void ReturnBook(BookId bookId, BookAmount amount)
        {
            var book = Books.First(a => a.BookId == bookId);
            if (book.BookAmount == amount)
            {
                Books.RemoveAll(a => a.BookId == bookId);
            }
            else if (book.BookAmount > amount)
            {
                Books.RemoveAll(a => a.BookId == bookId);
                Books.Add(new BookRecord(book.BookId, book.BookAmount - amount, book.Title));
            }
            else
            {
                throw new ArgumentException($"Cannot return book with title {book.Title.Value}. Amount to return is higher than {book.BookAmount.Amount}");
            }

            AddEvent(new BookRemovedFromLibraryRecord(bookId, amount.Amount, (LibraryRecordId)Id));

            if (ReturnDate < DateTime.UtcNow)
            {
                var oldFineValue = ReturnFine.Value;
                var daysBetweenTodayAndReturnDate = ((DateTime.UtcNow - ReturnDate)).Days;
                var newFineValue = daysBetweenTodayAndReturnDate * 10;
                ReturnFine = new ReturnFine(oldFineValue + newFineValue);
            }

            if (ReturnFine.Value == 0)
            {
                IsClosed = true;
            }
        }

        public void PayFine()
        {
            if (Books.Count == 0)
            {
                this.IsClosed = true;
            }
        }

        public override void CheckState()
        {
            if (Books == null ||
                User == null ||
                User.IsBanned)
            {
                throw new InvalidEntityStateException();
            }
        }

        public void UpdateBookTitle(string bookId, string newTitle)
        {
            var book = Books.Single(a => a.BookId.Value == bookId);
            Books.Add(new BookRecord(book.BookId, book.BookAmount, new BookTitle(newTitle)));
            Books.Remove(book);
        }
    }
}
