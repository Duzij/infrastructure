namespace Infrastructure.MongoDB
{
    public static class MongoUtils
    {
        public static string GetCollectionName<T>()
        {
            return typeof(T).FullName;
        }
    }
}
