using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDB
{
    public static class MongoUtils
    {
        public static string GetCollectionName<T>() => typeof(T).FullName;

    }
}
