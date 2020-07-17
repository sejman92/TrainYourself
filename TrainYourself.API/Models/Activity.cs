using System;
using System.Collections.Generic;

namespace TrainYourself.API.Models
{
    public class Activity : IMongoCommon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public IEnumerable<Metric> Metrics { get; set; }
    }
}
