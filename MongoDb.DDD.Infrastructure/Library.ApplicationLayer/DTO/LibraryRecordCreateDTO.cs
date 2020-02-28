using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer.DTO
{
    public class LibraryRecordCreateDTO
    {
        public List<BookRecord> Books { get; set; }
        public string UserId { get; set; }
    }

    public class BookRecord
    {
        public BookId BookId { get; set; }
        public int Amount { get; set; }
    }
}
