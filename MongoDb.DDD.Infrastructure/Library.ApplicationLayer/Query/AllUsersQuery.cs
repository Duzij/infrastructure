﻿using Infrastructure.MongoDB;
using Library.ApplicationLayer.DTO;
using Library.Domain.DomainAggregates;
using MongoDB.Driver;

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
