using System;
using MongoDB.Bson;
using TrainYourself.API.Common;

namespace TrainYourself.API.Models
{
    [BsonCollection("Metrics")]
    public class Metric : IDocument
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
        public DateTime ModifiedAt { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
