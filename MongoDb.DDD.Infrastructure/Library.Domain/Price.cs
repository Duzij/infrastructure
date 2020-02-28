using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class ReturnFine : Value<ReturnFine>
    {
        public decimal Value { get; private set; }

        public ReturnFine(decimal value)
        {
            if (value<0)
            {
                throw new ArgumentOutOfRangeException(nameof(ReturnFine) + "cannot be less than 0");
            }
            this.Value = value;
        }

    }
}
