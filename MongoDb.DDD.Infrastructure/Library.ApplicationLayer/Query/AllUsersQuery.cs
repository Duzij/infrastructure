using Infrastructure.MongoDb;
using Library.Domain;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.ApplicationLayer.Query
{
    public class AllUsersQuery : Query<UserDetailDTO>
    {
        public AllUsersQuery(IMongoDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IList<UserDetailDTO>> GetResultsAsync()
        {
            var users = dbContext.GetCollection<User>().AsQueryable();

            var userdetails = new List<UserDetailDTO>();
            foreach (var user in users)
            {
                userdetails.Add(new UserDetailDTO() { Id = user.Id.Value, Email = user.Email, IsBanned = user.IsBanned, Name = user.Name, Surname = user.Surname });
            }
            return userdetails;
        }
    }
}
