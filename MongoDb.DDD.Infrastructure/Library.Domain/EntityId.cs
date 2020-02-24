using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public class EntityId<T> : IId<Guid>
    {
        public Guid Value { get; set; }

        public EntityId(Guid value)
        {
            Value = value;
        }
    }
}
