using Infrastructure.Core;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ApplicationLayer
{
    public class BookFacade : IBookFacade
    {
        private readonly IRepository<Book> repository;

        public BookFacade(IRepository<Book> repository)
        {
            this.repository = repository;
        }
        public void Create(BookCreateDTO bookCreateDTO)
        {
            repository.Create(new Book(new Guid(), 10, 10, bookCreateDTO.Title, "Test", "Category", "Test Author"));
        }
    }
}
