using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Domain
{
    public class LibraryRecord : DomainAggregate
    {
        public User User { get; set; }
        public List<BookRecord> Books { get; private set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime ReturnDate => CreatedDate.AddDays(7);

        public ReturnFine ReturnFine { get; set; }

        public static LibraryRecord Create(User user, List<BookRecord> books)
        {
            var record = new LibraryRecord(TypedId.GetNewId<LibraryRecordId>(), user, books);
            return record;
        }
        private LibraryRecord(LibraryRecordId id, User user, List<BookRecord> books)
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
        }

        public void AddBook(BookId bookId, BookAmount amount, BookTitle title)
        {
            Books.Add(new BookRecord(bookId, amount, title));
            AddEvent(new BookAddedToLibraryRecord(bookId.Value, Id.Value));
        }

        public void ReturnBook(BookId bookId, BookAmount amount)
        {
            if (ReturnDate > DateTime.UtcNow)
            {
                var book = Books.First(a => a.BookId == bookId);
                if (book.BookAmount == amount)
                {
                    Books.RemoveAll(a => a.BookId == bookId);
                }
                else if(book.BookAmount > amount)
                {
                    Books.RemoveAll(a => a.BookId == bookId);
                    Books.Add(new BookRecord(book.BookId, book.BookAmount - amount, book.Title));
                }
                else
                {
                    throw new ArgumentException($"Cannot return book with title {book.Title.Value}. Amount to return is higher than {book.BookAmount.Amount}");
                }
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
            if (Books == null ||
                Books.Count == 0 ||
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
