namespace Library.Domain
{
    public class BookAddedToLibraryRecord
    {
        public LibraryRecordId LibraryRecordId { get; set; }
        public int BookStock { get; set; }
        public BookId BookId { get; set; }

        public BookAddedToLibraryRecord(BookId bookId, LibraryRecordId libraryRecordId, int bookStock)
        {
            this.LibraryRecordId = libraryRecordId;
            BookStock = bookStock;
            this.BookId = bookId;
        }
    }
}
