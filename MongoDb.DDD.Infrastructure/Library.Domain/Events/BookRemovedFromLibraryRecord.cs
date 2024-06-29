using Library.Domain.Id;

namespace Library.Domain.Events
{
    public class BookRemovedFromLibraryRecord
    {
        public LibraryRecordId LibraryRecordId { get; set; }
        public BookId BookId { get; set; }

        public int BookStock { get; set; }

        public BookRemovedFromLibraryRecord(BookId bookId, int bookStock, LibraryRecordId libraryRecordId)
        {
            LibraryRecordId = libraryRecordId;
            BookId = bookId;
            BookStock = bookStock;
        }
    }
}
