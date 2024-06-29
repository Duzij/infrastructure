namespace Library.ApplicationLayer.DTO
{
    public class AuthorDetailDTO
    {
        public AuthorDetailDTO()
        {
        }

        public AuthorDetailDTO(string id, string name, string surname, List<string> bookTitles)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BookTitles = bookTitles;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IList<string> BookTitles { get; set; } = [];
        public int BookCount => GetBooksCount();

        private int GetBooksCount()
        {
            if (BookTitles != null)
            {
                return BookTitles.Count;
            }
            return 0;
        }
    }
}
