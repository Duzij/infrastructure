﻿using Library.Domain;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public interface IBookFacade
    {
        Task Create(BookCreateDTO record);
        Task<List<BookDetailDTO>> GetBooks();
        Task Delete(string v);
        Task Update(BookDetailDTO bookDetail);
        Task<Dictionary<string, string>> GetBooksSelectorAsync();
        Task<BookDetailDTO> GetUserById(string value);
        Task UpdateAmount(string bookId, int amountValue);
        Task<List<BookId>> GetBookIdsByAuthorFullNameAsync(AuthorFullName oldName);
    }
}
