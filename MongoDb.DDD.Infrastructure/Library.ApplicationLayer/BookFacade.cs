using Infrastructure.Core;
using Infrastructure.MongoDB;
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
        private readonly AllBookDetailsQuery bookDetailsQuery;
        private readonly AllBooksQuery allBooksQuery;

        public BookFacade(IRepository<Book, string> repository, IRepository<Author, string> authorRepository, AllBookDetailsQuery bookQuery, AllBooksQuery allBooksQuery)
        {
            this.repository = repository;
            this.authorRepository = authorRepository;
            this.bookDetailsQuery = bookQuery;
            this.allBooksQuery = allBooksQuery;
        }
        public async Task Create(BookCreateDTO bookCreateDTO)
        {
            var book = Book.Create(bookCreateDTO.Title, bookCreateDTO.Description, new AuthorId(bookCreateDTO.AuthorId), new AuthorFullName(bookCreateDTO.AuthorName));
            await repository.InsertNewAsync(book);
        }

        public async Task Delete(string id)
        {
            await repository.RemoveAsync(new BookId(id));
        }

        public async Task<List<BookDetailDTO>> GetBooks()
        {
            var list = await bookDetailsQuery.GetResultsAsync();
            return list.ToList();
        }

        public async Task<Dictionary<string, string>> GetBooksSelectorAsync()
        {
            var books = await bookDetailsQuery.GetResultsAsync();
            var booksSelector = new Dictionary<string, string>();
            foreach (var book in books)
            {
                if (book.Amount > 0)
                {
                    booksSelector.Add(book.Id, $"{book.Title}");
                }
            }
            return booksSelector;
        }

        public async Task<BookDetailDTO> GetUserById(string id)
        {
            var book = await repository.GetByIdAsync(new BookId(id));
            return new BookDetailDTO(id, book.Title.Value, book.Description, book.AuthorName.ToString(), book.Amount.Amount, book.AuthorId.Value);
        }

        public async Task Update(BookDetailDTO bookDetail)
        {
            await repository.ModifyAsync(async book => 
            {
                if (bookDetail.AuthorId != book.AuthorId.Value)
                {
                    var author = await authorRepository.GetByIdAsync(new AuthorId(bookDetail.AuthorId));
                    var newAuthorName = new AuthorFullName(author.Name, author.Surname);
                    book.ChangeAuthor(newAuthorName, new AuthorId(bookDetail.AuthorId));
                }
                if (bookDetail.Title != book.Title.Value)
                {
                    book.ChangeTitle(new BookTitle(bookDetail.Title));
                }
                if (bookDetail.Description != book.Description)
                {
                    book.ChangeDescription(book.Description);
                }
            }, new BookId(bookDetail.Id));
        }

        public async Task UpdateAmount(string bookId, int amountValue)
        {
            await repository.ModifyAsync(book => book.AddStock(new BookAmount(amountValue)), new BookId(bookId));
        }

        public async Task<List<BookId>> GetBookIdsByAuthorFullNameAsync(AuthorFullName oldName)
        {
            var query = allBooksQuery;
            query.Filter = (a => a.AuthorName == oldName);
            var books = await query.GetResultsAsync();
            return books.Select(a => (BookId)a.Id).ToList();
        }
    }
}
