using Library.Domain;
using System;

namespace Library.ApplicationLayer
{
    public class BookCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }

    }
}