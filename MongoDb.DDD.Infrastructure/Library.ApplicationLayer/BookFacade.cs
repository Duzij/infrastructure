using Infrastructure.Core;
using Infrastructure.MongoDb;
using Library.ApplicationLayer.Query;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public class BookFacade : IBookFacade
    {
        private readonly IRepository<Book, string> repository;
        private readonly IRepository<Author, string> authorRepository;
        private readonly AllBooksQuery bookQuery;

        public BookFacade(IRepository<Book, string> repository, IRepository<Author, string> authorRepository, AllBooksQuery bookQuery)
        {
            this.repository = repository;
            this.authorRepository = authorRepository;
            this.bookQuery = bookQuery;
        }
        public async Task Create(BookCreateDTO bookCreateDTO)
        {
           var book = Book.Create(bookCreateDTO.Title, bookCreateDTO.Description, new AuthorId(bookCreateDTO.AuthorId), bookCreateDTO.AuthorName);
           await repository.InsertNewAsync(book);
        }

        public async Task Delete(string id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task<List<BookDetailDTO>> GetBooks()
        {
            var list = await bookQuery.GetResultsAsync();
            return list.ToList();
        }

        public async Task<Dictionary<string, string>> GetBooksSelectorAsync()
        {
            var books = await bookQuery.GetResultsAsync();
            var booksSelector = new Dictionary<string, string>();
            foreach (var book in books)
            {
                booksSelector.Add(book.Id, $"{book.Title}");
            }
            return booksSelector;
        }

        public async Task<BookDetailDTO> GetUserById(string id)
        {
            var book = await repository.GetByIdAsync(id);
            return new BookDetailDTO(id, book.Title.Value, book.Description, book.AuthorName, book.Amount.Amount, book.AuthorId.Value);
        }

        public async Task Update(BookDetailDTO bookDetail)
        {
             await repository.ModifyAsync(async book =>  {
                if (bookDetail.AuthorId != book.AuthorId.Value)
                {
                     var newAuthor = await authorRepository.GetByIdAsync(bookDetail.AuthorId);
                     var newAuthorName = $"{newAuthor.Name} {newAuthor.Surname}";
                     book.ChangeAuthor(new AuthorId(bookDetail.AuthorId), newAuthorName);
                }
                if (bookDetail.Title != book.Title.Value)
                {
                    book.ChangeTitle(new BookTitle(bookDetail.Title));
                }
            }, bookDetail.Id);
        }

        public async Task UpdateAmount(string bookId, int amountValue)
        {
            await repository.ModifyAsync(book=> book.AddStock(new BookAmount(amountValue)), bookId);
        }
    }
}
