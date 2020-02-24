using Library.Domain;
using System;

namespace Library.ApplicationLayer
{
    public class BookCreateDTO
    {
        public string Title { get; set; }

        public Guid AuthorId { get; set; }
    }
}