using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class Price : Value<Price>
    {
        public decimal Value { get; private set; }

        public Price(decimal value)
        {
            if (value<0)
            {
                throw new ArgumentOutOfRangeException(nameof(Price) + "cannot be less than 0");
            }
            this.Value = value;
        }

    }
}
