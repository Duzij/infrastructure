namespace Library.Domain
{
    public class BookAddedToLibraryRecord
    {
        public string LibraryRecordId { get; set; }
        public string BookId { get; set; }

        public BookAddedToLibraryRecord(string bookId, string libraryRecordId)
        {
            this.LibraryRecordId = libraryRecordId;
            this.BookId = bookId;
        }
    }
}
