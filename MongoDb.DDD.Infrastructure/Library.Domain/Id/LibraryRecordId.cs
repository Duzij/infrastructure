using Infrastructure.Core;

namespace Library.Domain
{
    public class LibraryRecordId : EntityId<LibraryRecord>
    {
        public LibraryRecordId(string value) : base(value)
        {
        }
    }
}
