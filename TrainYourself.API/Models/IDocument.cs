using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrainYourself.API.Models
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
        string Name { get; set; }
        DateTime CreatedAt { get; }
    }
}
