﻿namespace Infrastructure.MongoDb
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; }
    }
}