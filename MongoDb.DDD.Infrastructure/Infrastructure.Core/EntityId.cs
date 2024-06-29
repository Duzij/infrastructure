namespace Infrastructure.Core
{
    public class EntityId<T> : ValueObject, IId<string>
    {
        public string Value { get; private set; }
        public EntityId(string value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }
    }
}
