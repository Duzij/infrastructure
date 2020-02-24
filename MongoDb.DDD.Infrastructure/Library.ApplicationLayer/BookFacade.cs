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
           await repository.CreateAsync(new Book(Guid.NewGuid(), 10, 10, bookCreateDTO.Title, "Test", "Category", new AuthorId(bookCreateDTO.AuthorId.ToString())));
        }
    }
}
