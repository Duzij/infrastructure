
namespace Infrastructure.MongoDB
{
    public interface IMongoDbSettings
    {
        string ServerAddress { get; }
        string DatabaseName { get; }
    }
}