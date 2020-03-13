using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Infrastructure.Core
{
    public class EntityId<T> : Value<EntityId<T>>, IId<string>
    {
        public override bool Equals(object other)
        {
            return base.Equals(other);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public string Value { get; set; }
        public static bool operator == (EntityId<T> left, EntityId<T> right) => left.Value == right.Value;
        public static bool operator != (EntityId<T> left, EntityId<T> right) => left.Value != right.Value;

        public EntityId(string value)
        {
            Value = value;
        }
    }
}
