using Infrastructure.Core;

namespace Library.Domain
{
    public class AuthorId : EntityId<Author>
    {
        public AuthorId(string value) : base(value)
        {
        }
    }
}
