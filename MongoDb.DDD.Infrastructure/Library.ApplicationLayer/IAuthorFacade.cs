using Library.ApplicationLayer.DTO;
using Library.Domain;

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
        Task UpdateAuthorBooksAsync(string id, IList<AuthorBookRecord> books);
        Task<List<AuthorDetailDTO>> GetAuthorsByBookAsync(string title);
    }
}
