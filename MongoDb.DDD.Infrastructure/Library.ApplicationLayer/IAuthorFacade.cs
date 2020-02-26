using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public interface IAuthorFacade
    {
        public Task Create(CreateAuthorDTO user);
        Task<List<AuthorDetailDTO>> GetAuthors();
        Task Delete(string id);
        Task<AuthorDetailDTO> GetById(string v);
        Task Update(AuthorDetailDTO userDto);
        Task<Dictionary<string, string>> GetAuthorSelectorAsync();
        Task UpdateAuthorBooksAsync(string id, IList<string> bookTitles);
        Task<List<AuthorDetailDTO>> GetAuthorsByBookAsync(string title);
    }
}
