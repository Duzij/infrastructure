using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core
{
    public class EntityId<T> : IId<string>
    {
        public string Value { get; set; }

        public EntityId(string value)
        {
            Value = value;
        }
    }
}
