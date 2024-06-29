using Library.ApplicationLayer.DTO;

namespace Library.ApplicationLayer
{
    public interface IUserFacade
    {
        public Task Create(CreateUserDTO user);
        Task<List<UserDetailDTO>> GetUsers();
        Task Delete(string id);
        Task<UserDetailDTO> GetUserById(string v);
        Task Update(UserDetailDTO userDto);
        Task<Dictionary<string, string>> GetActiveUsersSelectorAsync();
    }
}
