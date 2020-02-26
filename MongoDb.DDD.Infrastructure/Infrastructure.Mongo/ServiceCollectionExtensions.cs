using Infrastructure.Core;
using Library.Domain;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.MongoDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbInfrastructure(this IServiceCollection services)
        {
            //default settings
            var projectDbName = Assembly.GetCallingAssembly().ManifestModule.Name;
            var index = projectDbName.AsSpan(0, projectDbName.IndexOf(".dll")).ToString();
            index = index.Replace(".", "");

            services.AddSingleton<IMongoDbSettings>(new MongoDbSettings(MongoDefaultSettings.ConnectionString, index));
            AddMongoDbInfrastructureServices(services);
            return services;
        }

        public static IServiceCollection AddMongoDbInfrastructure(this IServiceCollection services, MongoDbSettings settings = null)
        {
            services.AddSingleton<IMongoDbSettings>(settings);
            AddMongoDbInfrastructureServices(services);
            return services;
        }

        private static void AddMongoDbInfrastructureServices(IServiceCollection services)
        {
            services.AddTransient<EventWriter>();
            services.AddTransient<IMongoDbContext, MongoDbContext>();
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));

            services = services.Scan(scan => scan.FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)).Where(_ => !_.IsGenericType))
            .AsImplementedInterfaces().WithTransientLifetime());


            IEnumerable<Type> types =
            from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
            where
            type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IId<>)) &&
            type.IsClass
            select type;

            //foreach (var v in types)
            //{
            //    BsonClassMap.RegisterClassMap(new BsonClassMap(v));
            //}

            BsonClassMap.RegisterClassMap<BookId>();

            services.AddHostedService<EventHandlerBackgroundService>();
        }
    }
}
