﻿using System;
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

        public void ReturnBook(BookId bookId)
        {
            if (ReturnDate > DateTime.UtcNow)
            {
                Books.RemoveAll(a => a.BookId == bookId);
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

    public class BookRecord
    {
        public BookId BookId { get; }
        public BookAmount BookAmount { get; }
        public BookTitle Title { get; }

        public BookRecord(BookId bookId, BookAmount bookAmount, BookTitle title)
        {
            BookId = bookId;
            BookAmount = bookAmount;
            Title = title;
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
