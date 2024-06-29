using Infrastructure.MongoDB;
using Library.Domain.Id;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Library.ApplicationLayer
{
    public static class ApplicationServicesCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayerServices(this IServiceCollection services, string databaseName)
        {
            services.AddTransient<IBookFacade, BookFacade>();
            services.AddTransient<IAuthorFacade, AuthorFacade>();
            services.AddTransient<IUserFacade, UserFacade>();
            services.AddTransient<ILibraryRecordFacade, LibraryRecordFacade>();

            var settings = new MongoDbSettings("mongodb://localhost:27017?connect=replicaSet", databaseName);
            services.AddMongoDbInfrastructure(settings);

            BsonClassMap.RegisterClassMap(new BsonClassMap(typeof(AuthorId)));
            BsonClassMap.RegisterClassMap(new BsonClassMap(typeof(BookId)));
            BsonClassMap.RegisterClassMap(new BsonClassMap(typeof(LibraryRecordId)));
            BsonClassMap.RegisterClassMap(new BsonClassMap(typeof(UserId)));

            var objectSerializer = new ObjectSerializer(ObjectSerializer.AllAllowedTypes);
            BsonSerializer.RegisterSerializer(objectSerializer);

            return services;
        }
    }
}
