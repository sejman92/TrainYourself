using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainYourself.API.Models
{
    public class Activity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public IList<Metric> Metrics{ get; set; }
    }
}
