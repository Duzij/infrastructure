using Library.ApplicationLayer.DTO;
using Library.Domain;
using Library.Domain.DomainAggregates;

namespace Library.ApplicationLayer.Mappers
{
    public static class UserMapper
    {
        public static UserRecord MapTo(User user)
        {
            return new UserRecord(user.IsBanned, user.Name, user.Surname, user.Email);
        }
        public static UserRecord MapTo(UserDetailDTO user)
        {
            return new UserRecord(user.IsBanned, user.Name, user.Surname, user.Email);
        }
    }
}
