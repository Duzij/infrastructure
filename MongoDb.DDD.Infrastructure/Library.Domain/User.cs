using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class User : DomainAggregate
    {
        public bool IsNotBanned => !IsBanned;
        public bool IsBanned { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public static User Create(string name, string surname, string email)
        {
            return new User((UserId)TypedId.GetNewId<User>(), name, surname, email);
        }

        private User(UserId id, string name, string surname, string email)
        {
            Id = id;
            IsBanned = false;
            Name = name;
            Surname = surname;
            Email = email;
        }

        public void SetAsBanned()
        {
            this.IsBanned = true;
            AddEvent(new UserSetAsBanned(this.Id.Value, this.Email));
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

    public  class UserSetAsBanned
    {
        public string Value { get; set; }
        public string Email { get; set; }

        public UserSetAsBanned(string value, string email)
        {
            this.Value = value;
            this.Email = email;
        }
    }
}
