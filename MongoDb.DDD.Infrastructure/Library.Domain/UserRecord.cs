using Infrastructure.Core;

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
            return [IsBanned, Name, Surname, Email];
        }
    }
}
