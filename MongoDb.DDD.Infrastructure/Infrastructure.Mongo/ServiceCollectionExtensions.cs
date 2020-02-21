using Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static IServiceCollection AddMongoDbInfrastructure<TServiceConfigurator>(this IServiceCollection services, MongoDbSettings settings = null)
        {
            services.AddSingleton<IMongoDbSettings>(settings);
            AddMongoDbInfrastructureServices(services);
            return services;
        }

        private static void AddMongoDbInfrastructureServices(IServiceCollection services)
        {
            services.AddTransient<EventWriter>();
            services.AddTransient<IMongoDbContext, MongoDbContext>();
            services.AddTransient(typeof(IRepository<,>), typeof(Repository<>));
            services.AddHostedService<EventHandlerBackgroundService>();
        }
    }
}
