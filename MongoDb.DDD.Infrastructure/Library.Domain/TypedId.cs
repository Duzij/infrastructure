using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public static class TypedId
    {
        public static EntityId<T> GetNewId<T>()
        {
            var id = Activator.CreateInstance<EntityId<T>>();
            id.Value = Guid.NewGuid().ToString();
            return id;
        }
    }
}
