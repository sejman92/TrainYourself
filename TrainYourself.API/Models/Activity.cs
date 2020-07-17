using System;
using System.Collections.Generic;
using MongoDB.Bson;
using TrainYourself.API.Common;

namespace TrainYourself.API.Models
{
    [BsonCollection("Activities")]
    public class Activity : IDocument
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public IEnumerable<Metric> Metrics { get; set; }
    }
}
