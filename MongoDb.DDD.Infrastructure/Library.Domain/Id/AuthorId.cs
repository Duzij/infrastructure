using Infrastructure.Core;
using Library.Domain.DomainAggregates;

namespace Library.Domain.Id
{
    public class AuthorId : EntityId<Author>
    {
        public AuthorId(string value) : base(value)
        {
        }
    }
}
