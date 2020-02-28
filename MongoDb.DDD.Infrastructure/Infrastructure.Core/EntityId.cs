using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure.Core
{
    public class EntityId<T> : IId<string>, IEquatable<EntityId<T>>
    {
        public string Value { get; set; }

        public EntityId(string value)
        {
            Value = value;
        }

        public bool Equals([AllowNull] EntityId<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.Value == this.Value)
            {
                return true;
            }
            return false;
        }
    }
}
