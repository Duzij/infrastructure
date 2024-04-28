using Infrastructure.Core;

namespace Library.Tests
{
    public class CounterId : EntityId<string>
    {
        public CounterId(string value) : base(value)
        {
        }
    }
}
