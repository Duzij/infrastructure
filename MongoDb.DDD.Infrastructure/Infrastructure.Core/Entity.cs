using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Core
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public IId<TKey> Id { get; protected set; }
    }
}
