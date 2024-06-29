using Library.Domain.Events;
using Library.Domain.Id;

namespace Library.Domain.DomainAggregates
{
    public class User : AppAggregate
    {
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
            IsBanned = true;
        }

        public void UpdateUser(string name, string surname, string email)
        {
            var oldUser = new UserRecord(IsBanned, Name, Surname, Email);
            Name = name;
            Surname = surname;
            Email = email;
            var updatedUser = new UserRecord(IsBanned, name, surname, email);
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
            IsBanned = false;
        }
    }
}
