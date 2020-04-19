
namespace Infrastructure.MongoDB
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}