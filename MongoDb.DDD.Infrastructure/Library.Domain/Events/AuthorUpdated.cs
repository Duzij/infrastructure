using Library.Domain.Id;

namespace Library.Domain.Events
{
    public class AuthorUpdated
    {
        public AuthorId AuthorId { get; set; }
        public AuthorFullName oldName { get; set; }
        public AuthorFullName newName { get; set; }

        public AuthorUpdated(AuthorFullName oldName, AuthorFullName newName, AuthorId authorId)
        {
            this.oldName = oldName;
            this.newName = newName;
            AuthorId = authorId;
        }
    }
}
