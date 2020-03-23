namespace Library.Domain
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
            this.AuthorId = authorId;
        }
    }
}
