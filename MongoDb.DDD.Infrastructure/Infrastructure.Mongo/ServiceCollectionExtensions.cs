using Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Infrastructure.MongoDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDbSettings>(new MongoDbSettings("mongodb://localhost"));
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
        }
    }
}
