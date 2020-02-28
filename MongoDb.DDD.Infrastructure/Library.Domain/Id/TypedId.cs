using Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain
{
    public static class TypedId
    {
        public static T GetNewId<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Guid.NewGuid().ToString());
        }
    }
}
