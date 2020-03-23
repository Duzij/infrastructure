using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure.Core
{
    public class EntityId<T> : Value, IId<string>
    {
        public string Value { get; private set; }
        public EntityId(string value)
        {
            Value = value;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            return new List<object>() { this.Value };
        }
    }
}
