using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSystemApi.Model.Stat
{
    public struct SystemExecutionMetric
    {
        public int SecondsWaitTime { get; set; }
        public ExternalSystem System { get; set; }
        public int RequestCount { get; set; }
    }
}
