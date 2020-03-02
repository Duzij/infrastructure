using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDb
{
    public class EtagNotEqualException : Exception
    {
        public override string Message { get; }

        public EtagNotEqualException(Type entityType)
        {
            Message = string.Format("Entity of type {0} was changed during this operation. Try again later", entityType);

        }
    }
}
