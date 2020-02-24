using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class User : Entity
    {
        public bool IsNotBanned => !IsBanned;
        public bool IsBanned { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public User(Guid id, bool isBanned, string name, string surname, string email)
        {
            Id = new UserId(id.ToString());
            IsBanned = isBanned;
            Name = name;
            Surname = surname;
            Email = email;
        }

        public override void CheckState()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Email))
            {
                throw new InvalidEntityStateException();
            }
        }
    }

    public class UserId : IId<string>
    {
        public string Value { get; set; }

        public UserId(string id)
        {
            this.Value = id;
        }
    }
}
