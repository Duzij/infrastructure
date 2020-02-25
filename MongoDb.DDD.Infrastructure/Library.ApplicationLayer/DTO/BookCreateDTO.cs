using Library.Domain;
using System;

namespace Library.ApplicationLayer
{
    public class BookCreateDTO
    {
        public Guid Id;
        public string Title { get;  set; }
        public string Description { get;  set; }
        public string AuthorId { get; set; }
        public string AuthorName { get;  set; }

        public BookCreateDTO(Guid id)
        {
            this.Id = id;
        }

    }
}