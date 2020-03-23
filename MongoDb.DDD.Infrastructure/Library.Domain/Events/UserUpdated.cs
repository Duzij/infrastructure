namespace Library.Domain
{
    public class UserUpdated
    {
        public UserRecord oldUser { get; set; }
        public UserRecord updatedUser { get; set; }

        public UserUpdated(UserRecord oldUser, UserRecord updatedUser)
        {
            this.oldUser = oldUser;
            this.updatedUser = updatedUser;
        }
    }

}
