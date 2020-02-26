using Infrastructure.Core;
using Library.ApplicationLayer.Query;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public class UserFacade : IUserFacade
    {
        private readonly IRepository<User, string> repository;
        private readonly AllUsersQuery allUsersQuery;

        public UserFacade(IRepository<User, string> repository, AllUsersQuery allUsersQuery)
        {
            this.repository = repository;
            this.allUsersQuery = allUsersQuery;
        }
        public async Task Create(CreateUserDTO user)
        {
            var userEntity = User.Create(user.Name, user.Surname, user.Email);
            await repository.SaveAsync(userEntity);
        }

        public async Task Delete(string id)
        {
           await repository.RemoveAsync(id);
        }

        public async Task<UserDetailDTO> GetUserById(string v)
        {
            var user = await repository.GetByIdAsync(v);
            return new UserDetailDTO() { Id = user.Id.Value, Email = user.Email, IsBanned = user.IsBanned, Name = user.Name, Surname = user.Surname };
        }

        public async Task<List<UserDetailDTO>> GetUsers()
        {
            var users = await allUsersQuery.GetResultsAsync();
            return users.ToList();
        }

        public async Task Update(UserDetailDTO userDto)
        {
            var user = await repository.GetByIdAsync(userDto.Id);

            
        }

    }
}
