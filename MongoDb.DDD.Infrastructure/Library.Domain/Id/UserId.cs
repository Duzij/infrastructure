using Infrastructure.Core;
using Library.Domain.DomainAggregates;

namespace Library.Domain.Id
{
    public class UserId : EntityId<User>
    {
        public UserId(string value) : base(value)
        {
        }
    }
}
