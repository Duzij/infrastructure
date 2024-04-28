namespace Library.Domain
{
    public class BookRemovedFromLibraryRecord
    {
        public LibraryRecordId LibraryRecordId { get; set; }
        public BookId BookId { get; set; }

        public int BookStock { get; set; }

        public BookRemovedFromLibraryRecord(BookId bookId, int bookStock, LibraryRecordId libraryRecordId)
        {
            this.LibraryRecordId = libraryRecordId;
            this.BookId = bookId;
            BookStock = bookStock;
        }
    }
}
