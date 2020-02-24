using Infrastructure.Core;
using System;

namespace Library.Domain
{
    public class BookAmount : Value<BookAmount>
    {
        public int Amount { get; private set; }

        public BookAmount(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(Price) + "cannot be less than 0");
            }
            this.Amount = amount;
        }
    }
}