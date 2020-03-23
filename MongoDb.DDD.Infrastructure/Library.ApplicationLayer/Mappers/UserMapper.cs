using Library.Domain;
using System;
using System.Collections.Generic;
using System.Text;

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
