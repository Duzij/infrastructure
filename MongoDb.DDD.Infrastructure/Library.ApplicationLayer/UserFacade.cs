using Infrastructure.Core;
using Library.ApplicationLayer.Mappers;
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
        private readonly AllLibraryRecordsQuery allLibraryRecordsQuery;

        public UserFacade(IRepository<User, string> repository, AllUsersQuery allUsersQuery, AllLibraryRecordsQuery allLibraryRecordsQuery)
        {
            this.repository = repository;
            this.allUsersQuery = allUsersQuery;
            this.allLibraryRecordsQuery = allLibraryRecordsQuery;
        }
        public async Task Create(CreateUserDTO user)
        {
            var userEntity = User.Create(user.Name, user.Surname, user.Email);
            await repository.InsertNewAsync(userEntity);
        }

        public async Task Delete(string id)
        {
            await repository.RemoveAsync(new UserId(id));
        }

        public async Task<UserDetailDTO> GetUserById(string id)
        {
            var user = await repository.GetByIdAsync(new UserId(id));
            return new UserDetailDTO() { Id = user.Id.Value, Email = user.Email, IsBanned = user.IsBanned, Name = user.Name, Surname = user.Surname };
        }

        public async Task<List<UserDetailDTO>> GetUsers()
        {
            var users = await allUsersQuery.GetResultsAsync();
            return users.ToList();
        }

        public async Task<Dictionary<string, string>> GetActiveUsersSelectorAsync()
        {
            var users = await allUsersQuery.GetResultsAsync();
            var userSelector = new Dictionary<string, string>();
            foreach (var user in users)
            {
                if (!user.IsBanned)
                {
                    userSelector.Add(user.Id, $"{user.Name} {user.Surname}");
                }
            }
            return userSelector;
        }

        public async Task Update(UserDetailDTO userDto)
        {
            var allRecords = await allLibraryRecordsQuery.GetResultsAsync();
            var userHasSomeRecords = allRecords.Any(a => a.User == UserMapper.MapTo(userDto) && !a.IsClosed);

            await repository.ModifyAsync(user =>
            {
                if (userDto.IsBanned)
                {
                    if (userHasSomeRecords)
                    {
                        throw new InvalidOperationException(ErrorMessages.UserCannotBeBanned);
                    }
                    else
                    {
                        user.SetAsBanned();
                    }
                }
                else
                {
                    user.Unban();
                }
                user.UpdateUser(userDto.Name, userDto.Surname, userDto.Email);
            }, new UserId(userDto.Id));
        }

    }
}
