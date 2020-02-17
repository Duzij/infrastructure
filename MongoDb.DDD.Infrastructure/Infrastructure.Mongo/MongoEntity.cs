using Infrastructure.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Infrastructure.MongoDb
{
    public class MongoEntity : Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; internal set; }

        public override bool CheckState()
        {
            if (Id != null)
            {
                return true;
            }

            return false;
        }
    }
}