using System;

namespace TrainYourself.API.Models
{
    public class TrainingSession
    {
        public Training Training { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
