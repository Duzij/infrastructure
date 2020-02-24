using Infrastructure.Core;
using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer
{
    public class UserFacade : IUserFacade
    {
        private readonly IRepository<User, string> repository;

        public UserFacade(IRepository<User, string> repository)
        {
            this.repository = repository;
        }
        public async Task Create(CreateUserDTO user)
        {
          await repository.CreateAsync(new User(user.Id, false, user.Name, user.Surname, user.Email));
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
            var users = await repository.GetAsync();
            var userdetails = new List<UserDetailDTO>();
            foreach (var user in users)
            {
                userdetails.Add(new UserDetailDTO() { Id = user.Id.Value, Email = user.Email, IsBanned = user.IsBanned, Name = user.Name, Surname = user.Surname });
            }
            return userdetails;
        }

        public async Task Update(UserDetailDTO userDto)
        {
           await repository.GetAndModify(userDto.Id, (a) => { return new User(Guid.Parse(userDto.Id), userDto.IsBanned, userDto.Name, userDto.Surname, userDto.Email); });
        }

    }
}
