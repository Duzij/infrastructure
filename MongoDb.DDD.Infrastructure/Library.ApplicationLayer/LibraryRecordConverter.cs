﻿using Library.ApplicationLayer.DTO;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer
{
    public static class LibraryRecordConverter
    {
        public static LibraryRecordDetailDTO Convert(LibraryRecord record)
        {
            var books = new List<BookRecordDTO>();

            foreach (var book in record.Books)
            {
                books.Add(new BookRecordDTO()
                {
                    id = book.BookId.Value,
                    amount = book.BookAmount.Amount.ToString(),
                    title = book.Title.Value
                });
            }

            return new LibraryRecordDetailDTO()
            {
                Id = record.Id.Value,
                Books = books,
                CreatedDate = record.CreatedDate,
                IsExpired = DateTime.UtcNow > record.ReturnDate,
                ReturnDate = record.ReturnDate,
                Username = record.User.Name + " " + record.User.Surname
            };
        }
    }
}