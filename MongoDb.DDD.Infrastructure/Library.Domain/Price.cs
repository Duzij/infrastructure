using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Price : Value<Price>
    {
        private readonly decimal amount;

        public Price(decimal amount)
        {
            if (amount<0)
            {
                throw new ArgumentOutOfRangeException(nameof(Price) + "cannot be less than 0");
            }
            this.amount = amount;
        }

    }
}
