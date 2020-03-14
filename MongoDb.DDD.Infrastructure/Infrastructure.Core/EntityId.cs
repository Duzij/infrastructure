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
        public static bool operator ==(EntityId<T> left, EntityId<T> right) => IdsEqual(left, right);
        public static bool operator !=(EntityId<T> left, EntityId<T> right) => IdNotEqual(left, right);

        private static bool IdNotEqual(EntityId<T> left, EntityId<T> right)
        {
            return !IdsEqual(left, right);
        }

        private static bool IdsEqual(EntityId<T> left, EntityId<T> right)
        {
            var isLeftNull = object.ReferenceEquals(left, null);
            var isRightNull = object.ReferenceEquals(right, null);
            if (isLeftNull)
            {
                if (isRightNull)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (isRightNull)
                {
                    return false;
                }
                else
                {
                    return left.Value == right.Value;
                }
            }
        }


        public EntityId(string value)
        {
            Value = value;
        }
    }
}
