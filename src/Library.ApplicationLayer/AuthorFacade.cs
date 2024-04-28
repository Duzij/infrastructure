using Infrastructure.Core;
using Library.ApplicationLayer.Mappers;
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
        private readonly AuthorByBookTitleQuery query;
        private readonly AllAuthorsQuery allAuthorsQuery;

        public AuthorFacade(IRepository<Author, string> repository, AuthorByBookTitleQuery query, AllAuthorsQuery allAuthorsQuery)
        {
            this.repository = repository;
            this.query = query;
            this.allAuthorsQuery = allAuthorsQuery;
        }

        public async Task Create(CreateAuthorDTO author)
        {
            var authorEntity = Author.Create(new AuthorFullName(author.Name, author.Surname));
            await repository.InsertNewAsync(authorEntity);
        }

        public async Task Delete(string id)
        {
            await repository.RemoveAsync(new AuthorId(id));
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

        public async Task<AuthorDetailDTO> GetById(string id)
        {
            var author = await repository.GetByIdAsync(new AuthorId(id));
            return AuthorMapper.MapTo(author);
        }

        public async Task Update(AuthorDetailDTO authorDto)
        {
            await repository.ModifyAsync(author =>
            {
                author.UpdateAuthorFullName(new AuthorFullName(authorDto.Name, authorDto.Surname));
            }, new AuthorId(authorDto.Id));
        }

        public async Task UpdateAuthorBooksAsync(string id, IList<AuthorBookRecord> bookTitles)
        {
            await repository.ModifyAsync(authorEntity => authorEntity.UpdateBooks(bookTitles), new AuthorId(id));
        }
    }
}
