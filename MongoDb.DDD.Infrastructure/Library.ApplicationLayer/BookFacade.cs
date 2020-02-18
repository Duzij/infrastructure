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
        private readonly Repository<Book> repository;

        public BookFacade(Repository<Book> repository)
        {
            this.repository = repository;
        }
        public async Task Create(BookCreateDTO bookCreateDTO)
        {
           await repository.Create(new Book(new Guid(), 10, 10, bookCreateDTO.Title, "Test", "Category", "Test Author"));
        }
    }
}
