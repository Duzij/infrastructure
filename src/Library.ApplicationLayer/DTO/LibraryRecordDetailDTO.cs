using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer.DTO
{
    public class LibraryRecordDetailDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal ReturnFine { get; set; }
        public bool ShowReturnFineButton => !(ReturnFine > 0);
        public bool ShowReturnBooksButton => Books.Count > 0;
        public List<BookRecordDTO> Books { get; set; }
        public bool IsExpired { get; set; }
    }
}
