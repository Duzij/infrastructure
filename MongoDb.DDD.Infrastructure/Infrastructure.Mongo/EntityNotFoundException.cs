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
            Message = "Entity was not found.";
        }

        public EntityNotFoundException(Type entityType, string id)
        {
            Message = string.Format("{0} was not found with id {1}.", entityType, id);
        }

        public EntityNotFoundException(string message) : base(message)
        {
            Message = message;
        }

        public EntityNotFoundException(Type entityType)
        {
            Message = string.Format("Entity of type {0} was not found.", entityType);
        }
    }
}
