using Infrastructure.Core;
using Infrastructure.MongoDb;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public class BookFacade : IBookFacade
    {
        private readonly IRepository<Book, string> repository;

        public BookFacade(IRepository<Book, string> repository)
        {
            this.repository = repository;
        }
        public async Task Create(BookCreateDTO bookCreateDTO)
        {
           var book = new Book(bookCreateDTO.Id, bookCreateDTO.Title, bookCreateDTO.Description, new AuthorId(bookCreateDTO.AuthorId), bookCreateDTO.AuthorName);
           await repository.CreateAsync(book);
        }

        public async Task Delete(string id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task<List<BookDetailDTO>> GetBooks()
        {
            var books = await repository.GetAsync();
            var userdetails = new List<BookDetailDTO>();
            foreach (var book in books)
            {
                userdetails.Add(new BookDetailDTO(book.Id.Value, book.Title, book.Description, book.AuthorName, book.Amount.Amount));
            }
            return userdetails;
        }
    }
}
