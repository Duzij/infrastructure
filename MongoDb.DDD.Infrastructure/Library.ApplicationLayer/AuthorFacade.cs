using Infrastructure.Core;
using Library.ApplicationLayer.Query;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public class AuthorFacade : IAuthorFacade
    {
        private readonly IRepository<Author, string> repository;
        private readonly IRepository<Book, string> bookRepository;
        private readonly AuthorByBookTitleQuery query;
        private readonly AllAuthorsQuery allAuthorsQuery;

        public AuthorFacade(IRepository<Author,string> repository, IRepository<Book, string> bookRepository, AuthorByBookTitleQuery query, AllAuthorsQuery allAuthorsQuery)
        {
            this.repository = repository;
            this.bookRepository = bookRepository;
            this.query = query;
            this.allAuthorsQuery = allAuthorsQuery;
        }

        public async Task Create(CreateAuthorDTO author)
        {
            var authorEntity = Author.Create(author.Name, author.Surname);
            await repository.InsertNewAsync(authorEntity);
        }

        public async Task Delete(string id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task<List<AuthorDetailDTO>> GetAuthors()
        {
            var list = await allAuthorsQuery.GetResultsAsync();
            return list.ToList();
        }

        public async Task<List<AuthorDetailDTO>> GetAuthorsByBookAsync(string BookId)
        {
            query.BookId = new BookId(BookId);
            var list = await query.GetResultsAsync();
            return list.ToList();
        }

        public async Task<Dictionary<string, string>> GetAuthorSelectorAsync()
        {
            var authors = await allAuthorsQuery.GetResultsAsync();
            var authorsSelector = new Dictionary<string, string>();
            foreach (var author in authors)
            {
                authorsSelector.Add(author.Id, $"{author.Name} {author.Surname}");
            }
            return authorsSelector;
        }

        public async Task<AuthorDetailDTO> GetById(string v)
        {
            var author = await repository.GetByIdAsync(v);

            var books = new List<string>();

            foreach (var bookId in author.Books)
            {
                var book = await bookRepository.GetByIdAsync(bookId.Value);
                books.Add(book.Title.Value);
            }

            return new AuthorDetailDTO(author.Id.Value, author.Name, author.Surname, books);
        }

        public async Task Update(AuthorDetailDTO author)
        {
            await repository.ReplaceAsync(authorEntity => {
                if (authorEntity.Name != author.Name)
                {
                    authorEntity.ChangeName(author.Name);
                }
                if (authorEntity.Surname != author.Surname)
                {
                    authorEntity.ChangeSurname(author.Surname);
                }
            }, author.Id);
        }

        public async Task UpdateAuthorBooksAsync(string id, IList<BookId> bookTitles)
        {
            await repository.ReplaceAsync(authorEntity => authorEntity.UpdateBooks(bookTitles), id);
        }
    }
}
