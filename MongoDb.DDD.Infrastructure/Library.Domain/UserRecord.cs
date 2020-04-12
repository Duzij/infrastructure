using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class UserRecord : ValueObject
    {
        public bool IsBanned;
        public string Name;
        public string Surname;
        public string Email;
        public bool IsNotBanned => !IsBanned;


        public UserRecord(bool isBanned, string name, string surname, string email)
        {
            IsBanned = isBanned;
            Name = name;
            Surname = surname;
            Email = email;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new List<object>() { this.IsBanned, this.Name, this.Surname, this.Email };
        }
    }
}
