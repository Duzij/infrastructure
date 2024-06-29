namespace Library.Domain.Id
{
    public static class TypedId
    {
        public static T GetNewId<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Guid.NewGuid().ToString());
        }
    }
}
