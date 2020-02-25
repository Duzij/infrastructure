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
        private readonly IAuthorByBookTitleQuery query;

        public AuthorFacade(IRepository<Author,string> repository, IAuthorByBookTitleQuery query)
        {
            this.repository = repository;
            this.query = query;
        }
        public async Task Create(CreateAuthorDTO author)
        {
            await repository.CreateAsync(new Author(author.Id, author.Name, author.Surname));
        }

        public async Task Delete(string id)
        {
            await repository.RemoveAsync(id);
        }

        public async Task<List<AuthorDetailDTO>> GetAuthors()
        {
            var authors = await repository.GetAsync();
            var authorDetails = new List<AuthorDetailDTO>();
            foreach (var author in authors)
            {
                authorDetails.Add(new AuthorDetailDTO(author.Id.Value, author.Name,author.Surname,author.BookTitles));
            }
            return authorDetails;
        }

        public List<AuthorDetailDTO> GetAuthorsByBookId(string bookTitle)
        {
            query.BookTitle = bookTitle;
            return query.GetResults().ToList();
        }

        public async Task<Dictionary<string, string>> GetAuthorSelectorAsync()
        {
            var authors = await repository.GetAsync();
            var authorsSelector = new Dictionary<string, string>();
            foreach (var author in authors)
            {
                authorsSelector.Add(author.Id.Value, $"{author.Name} {author.Surname}");
            }
            return authorsSelector;
        }

        public async Task<AuthorDetailDTO> GetById(string v)
        {
            var author = await repository.GetByIdAsync(v);
            return new AuthorDetailDTO(author.Id.Value, author.Name, author.Surname, author.BookTitles);
        }

        public async Task Update(AuthorDetailDTO author)
        {
            await repository.GetAndModify(author.Id, (a) => { return new Author(Guid.Parse(author.Id), author.BookTitles, author.Name, author.Surname); });
        }

        public async Task UpdateAuthorBooksAsync(string id, IList<string> bookTitles)
        {
            await repository.GetAndModify(id, (a) => { return new Author(Guid.Parse(a.Id.Value), bookTitles, a.Name, a.Surname); });
        }
    }
}
