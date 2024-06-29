using Infrastructure.Core;
using Library.Domain.DomainAggregates;

namespace Library.Domain.Id
{
    public class BookId : EntityId<Book>
    {
        public BookId(string value) : base(value)
        {
        }
    }
}
