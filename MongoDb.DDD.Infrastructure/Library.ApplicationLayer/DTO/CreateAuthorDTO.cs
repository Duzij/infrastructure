namespace Library.ApplicationLayer.DTO
{
    public class CreateAuthorDTO
    {
        public Guid Id;

        public List<string> BookTitles { get; set; } = [];
        public CreateAuthorDTO(Guid id)
        {
            Id = id;
        }

        public CreateAuthorDTO(Guid id, List<string> bookTitles)
        {
            Id = id;
            BookTitles = bookTitles;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
