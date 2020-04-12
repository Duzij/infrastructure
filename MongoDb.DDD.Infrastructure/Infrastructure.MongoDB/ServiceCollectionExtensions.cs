using Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.MongoDB
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
            services.AddTransient<IMongoDbContext, MongoDbContext>();
            services.AddHostedService<EventHandlerBackgroundService>();

            services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));

            services.Scan(scan => scan.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());


            services.Scan(scan => scan.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(Query<>)))
            .AsSelf()
            .WithTransientLifetime());
        }
    }
}
