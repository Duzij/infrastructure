using Infrastructure.Core;

namespace Library.Domain
{
    public class ReturnFine : ValueObject
    {
        public decimal Value;

        public ReturnFine(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ReturnFine) + "cannot be less than 0");
            }
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Value];
        }
    }
}
