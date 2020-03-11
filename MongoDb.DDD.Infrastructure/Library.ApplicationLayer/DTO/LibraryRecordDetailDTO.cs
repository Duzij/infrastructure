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
        public List<BookRecordDTO> Books { get; set; }
        public bool IsExpired { get; set; }
    }
}
