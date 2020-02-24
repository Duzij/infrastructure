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
    }
}
