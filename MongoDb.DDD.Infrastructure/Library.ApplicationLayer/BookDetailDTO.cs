﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer
{
    public class BookDetailDTO
    {
        public BookDetailDTO(string bookId, string title, string description, string authorName, int amount)
        {
            Id = bookId;
            Title = title;
            Description = description;
            AuthorName = authorName;
            Amount = amount;
        }

        public string Id { get;  set; }
        public string Title { get;  set; }
        public string Description { get;  set; }
        public string AuthorName { get;  set; }
        public int Amount { get;  set; }
    }
}