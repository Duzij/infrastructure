using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.Mongo
{
    public interface IMongoEntity<T> where T : class
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public T Id { get; set; }
    }
}
