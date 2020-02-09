using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Domain
{
    public class Price : Entity
    {
        private readonly decimal amount;

        public Price(decimal amount)
        {
            this.amount = amount;
        }

        public override bool IsValid()
        {
            return amount < 0;
        }
    }
}
