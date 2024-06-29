namespace Library.ApplicationLayer.DTO
{
    public class LibraryRecordCreateDTO
    {
        public string userId { get; set; }
        public List<BookRecordDTO> books { get; set; }
    }

    public class BookRecordDTO
    {
        public string title { get; set; }
        public string id { get; set; }
        public string amount { get; set; }
    }
}
