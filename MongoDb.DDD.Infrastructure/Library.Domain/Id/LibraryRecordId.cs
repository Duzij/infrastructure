using Infrastructure.Core;
using Library.Domain.DomainAggregates;

namespace Library.Domain.Id
{
    public class LibraryRecordId : EntityId<LibraryRecord>
    {
        public LibraryRecordId(string value) : base(value)
        {
        }
    }
}
