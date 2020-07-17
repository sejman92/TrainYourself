using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TrainYourself.API.Models
{
    public class Training
    {
        [BsonId]
        public string Id { get; set; }
        public IList<Activity> Activities { get; set; }
        public DateTime Date { get; set; }
    }
}
