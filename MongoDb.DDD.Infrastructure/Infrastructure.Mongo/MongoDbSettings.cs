namespace Infrastructure.MongoDb
{
    public class MongoDbSettings : IMongoDbSettings
    {

        public MongoDbSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}