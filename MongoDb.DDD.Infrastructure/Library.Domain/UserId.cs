using Infrastructure.Core;

namespace Library.Domain
{
    public class UserId : EntityId<User>
    {
        public UserId(string value) : base(value)
        {
        }
    }
}
