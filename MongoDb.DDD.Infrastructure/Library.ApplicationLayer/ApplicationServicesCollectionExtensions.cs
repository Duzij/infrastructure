﻿using Infrastructure.Core;
using Infrastructure.MongoDB;
using Library.ApplicationLayer;
using Library.ApplicationLayer.Query;
using Library.Domain;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;

namespace Infrastructure.ApplicationLayer
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

            return services;
        }
    }
}
