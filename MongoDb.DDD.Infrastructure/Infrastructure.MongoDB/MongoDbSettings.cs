
namespace Infrastructure.MongoDB
{
    public class MongoDbSettings : IMongoDbSettings
    {
 

        public MongoDbSettings(string connectionString, string databaseName)
        {
            ServerAddress = connectionString;
            DatabaseName = databaseName;
        }

        public string ServerAddress { get; set; }

        public string DatabaseName { get; set; }
    }
}