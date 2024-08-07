﻿namespace Infrastructure.MongoDB.Exception
{
    public class EtagNotEqualException : System.Exception
    {
        public override string Message { get; }

        public EtagNotEqualException(Type entityType)
        {
            Message = string.Format("Entity of type {0} was changed during this operation. Try again later", entityType);

        }
    }
}
