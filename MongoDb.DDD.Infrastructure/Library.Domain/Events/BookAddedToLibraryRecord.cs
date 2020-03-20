namespace Library.Domain
{
    public class BookAddedToLibraryRecord
    {
        public string LibraryRecordId { get; set; }
        public int BookStock { get; set; }
        public string BookId { get; set; }

        public BookAddedToLibraryRecord(string bookId, string libraryRecordId, int bookStock)
        {
            this.LibraryRecordId = libraryRecordId;
            BookStock = bookStock;
            this.BookId = bookId;
        }
    }
}
