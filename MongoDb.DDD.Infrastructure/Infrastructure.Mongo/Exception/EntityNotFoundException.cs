using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.MongoDb
{
    public class EntityNotFoundException : Exception
    {

        public override string Message { get; }

        public EntityNotFoundException()
        {
            Message = Messages.EntityWasNotFoundMessage;
        }

        public EntityNotFoundException(Type entityType, string id)
        {
            Message = string.Format(Messages.EntityWithTypeAndIdNotFound, entityType, id);
        }

        public EntityNotFoundException(string message) : base(message)
        {
            Message = message;
        }

        public EntityNotFoundException(Type entityType)
        {
            Message = string.Format(Messages.EntityWithTypeNotFound, entityType);
        }
    }
}
