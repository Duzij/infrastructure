namespace Infrastructure.MongoDB.Exception
{
    public class EntityNotFoundException : System.Exception
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
