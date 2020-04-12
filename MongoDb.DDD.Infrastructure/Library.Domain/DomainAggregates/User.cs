using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class User : AppAggregate
    {
        public bool IsNotBanned => !IsBanned;
        public bool IsBanned { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }

        public static User Create(string name, string surname, string email)
        {
            return new User(TypedId.GetNewId<UserId>(), name, surname, email);
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
        }

        public void UpdateUser(string name, string surname, string email)
        {
            var oldUser = new UserRecord(this.IsBanned, this.Name, this.Surname, this.Email);
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            var updatedUser = new UserRecord(this.IsBanned, name, surname, email);
            AddEvent(new UserUpdated(oldUser, updatedUser));
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

        public void Unban()
        {
            this.IsBanned = false;
        }
    }

}
