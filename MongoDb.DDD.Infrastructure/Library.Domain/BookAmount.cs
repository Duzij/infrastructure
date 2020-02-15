using Infrastructure.Core;
using System;

namespace Library.Domain
{
    public class BookAmount : Value<BookAmount>
    {
        public int Amount { get; private set; }

        public BookAmount(int amount)
        {
            if (Amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Price) + "cannot be less or equal to 0");
            }
            this.Amount = amount;
        }
    }
}