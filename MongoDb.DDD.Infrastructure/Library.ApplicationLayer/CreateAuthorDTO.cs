using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer
{
    public class CreateAuthorDTO
    {
        public Guid Id;

        public List<string> BookTitles { get; set; } = new List<string>();
        public CreateAuthorDTO(Guid id)
        {
            this.Id = id;
        }

        public CreateAuthorDTO(Guid id, List<string> bookTitles)
        {
            this.Id = id;
            BookTitles = bookTitles;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
