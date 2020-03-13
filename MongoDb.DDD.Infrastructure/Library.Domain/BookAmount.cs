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
                throw new ArgumentException(nameof(BookAmount) + "cannot be less than 0");
            }
            this.Amount = amount;
        }

        public static bool operator > (BookAmount left, BookAmount right) => left.Amount > right.Amount;
        public static bool operator < (BookAmount left, BookAmount right) => left.Amount < right.Amount;
        public static BookAmount operator - (BookAmount left, BookAmount right) => new BookAmount(left.Amount - right.Amount);
        public static BookAmount operator + (BookAmount left, BookAmount right) => new BookAmount(left.Amount + right.Amount);

    }
}