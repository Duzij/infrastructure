namespace Library.Domain
{
    public class BookRemovedFromLibraryRecord
    {
        public string LibraryRecordId { get; set; }
        public string BookId { get; set; }

        public BookRemovedFromLibraryRecord(string bookId, string libraryRecordId)
        {
            this.LibraryRecordId = libraryRecordId;
            this.BookId = bookId;
        }
    }
}
