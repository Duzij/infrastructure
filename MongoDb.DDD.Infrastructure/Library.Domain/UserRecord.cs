using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class UserRecord : ValueObject
    {
        public bool IsBanned { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
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
