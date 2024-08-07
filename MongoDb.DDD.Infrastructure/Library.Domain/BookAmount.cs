﻿using Infrastructure.Core;

namespace Library.Domain
{
    public class BookAmount : ValueObject
    {
        public int Amount { get; private set; }

        public BookAmount(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(BookAmount) + "cannot be less than 0");
            }

            Amount = amount;
        }

        public static bool operator >(BookAmount left, BookAmount right)
        {
            return left.Amount > right.Amount;
        }

        public static bool operator <(BookAmount left, BookAmount right)
        {
            return left.Amount < right.Amount;
        }

        public static BookAmount operator -(BookAmount left, BookAmount right)
        {
            return new BookAmount(left.Amount - right.Amount);
        }

        public static BookAmount operator +(BookAmount left, BookAmount right)
        {
            return new BookAmount(left.Amount + right.Amount);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return [Amount];
        }
    }
}