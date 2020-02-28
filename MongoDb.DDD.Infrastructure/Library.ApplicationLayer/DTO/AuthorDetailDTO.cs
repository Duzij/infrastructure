using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer
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
        [BindNever]
        public IList<string> BookTitles { get; set; } = new List<string>();
        public int BookCount { get { return GetBooksCount(); } }

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
