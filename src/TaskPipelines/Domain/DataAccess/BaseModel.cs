using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskPipelines.Domain.DataAccess
{
    public abstract class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}