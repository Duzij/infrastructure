
namespace Infrastructure.MongoDB
{
    public class MongoDbSettings : IMongoDbSettings
    {


        public MongoDbSettings(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}