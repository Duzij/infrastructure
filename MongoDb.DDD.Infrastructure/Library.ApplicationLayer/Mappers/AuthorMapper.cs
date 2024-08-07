﻿using Library.ApplicationLayer.DTO;
using Library.Domain.DomainAggregates;

namespace Library.ApplicationLayer.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorDetailDTO MapTo(Author author)
        {
            var books = new List<string>();

            foreach (var book in author.Books)
            {
                books.Add(book.Title.Value);
            }

            return new AuthorDetailDTO(author.Id.Value, author.authorFullName.Name, author.authorFullName.Surname, books);
        }

        public static string GetAuthorName(string name, string surname)
        {
            return $"{name} {surname}";
        }
    }
}
