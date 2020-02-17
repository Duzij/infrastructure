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
            AddMongoDbInfrastructureServices(services);
            return services;
        }

        public static IServiceCollection AddMongoDbInfrastructure<TServiceConfigurator>(this IServiceCollection services, MongoDbSettings settings = null)
        {
            AddMongoDbInfrastructureServices(services);
            if (settings == null)
            {
                services.AddSingleton<IMongoDbSettings>(new MongoDbSettings("mongodb://localhost"));
            }
            else
            {
                services.AddSingleton<IMongoDbSettings>(settings);
            }

            return services;
        }

        private static void AddMongoDbInfrastructureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
