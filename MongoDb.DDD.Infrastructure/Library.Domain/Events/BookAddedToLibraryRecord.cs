using Library.Domain.Id;

namespace Library.Domain.Events
{
    public class BookAddedToLibraryRecord
    {
        public LibraryRecordId LibraryRecordId { get; set; }
        public int BookStock { get; set; }
        public BookId BookId { get; set; }

        public BookAddedToLibraryRecord(BookId bookId, LibraryRecordId libraryRecordId, int bookStock)
        {
            LibraryRecordId = libraryRecordId;
            BookStock = bookStock;
            BookId = bookId;
        }
    }
}
