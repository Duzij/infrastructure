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
        private readonly IRepository<Author, string> authorRepository;

        public BookFacade(IRepository<Book, string> repository, IRepository<Author, string> authorRepository)
        {
            this.repository = repository;
            this.authorRepository = authorRepository;
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
                userdetails.Add(new BookDetailDTO(book.Id.Value, book.Title.Value, book.Description, book.AuthorName, book.Amount.Amount, book.AuthorId.Value));
            }
            return userdetails;
        }

        public async Task<BookDetailDTO> GetUserById(string id)
        {
            var book = await repository.GetByIdAsync(id);
            return new BookDetailDTO(id, book.Title.Value, book.Description, book.AuthorName, book.Amount.Amount, book.AuthorId.Value);
        }

        public async Task Update(BookDetailDTO bookDetail)
        {
            var book = await repository.GetByIdAsync(bookDetail.Id);
            var newAuthor = await authorRepository.GetByIdAsync(bookDetail.AuthorId);
            var newAuthorName = $"{newAuthor.Name} {newAuthor.Surname}";

            if (bookDetail.AuthorId != book.AuthorId.Value)
            {
                book.ChangeAuthor(new AuthorId(bookDetail.AuthorId), newAuthorName);
            }
            if (bookDetail.Title != book.Title.Value)
            {
                book.ChangeTitle(new BookTitle(bookDetail.Title));
            }

            await repository.GetAndModify(bookDetail.Id,
                 (a) => { return book; });
        }
    }
}
