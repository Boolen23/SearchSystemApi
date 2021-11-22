using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSystemApi.Model.Stat
{
    public struct ExecutionStatistic
    {
        public ExternalSystem SystemName { get; set; }
        public ResponseStatus Status { get; set; }
        public int WaitTime { get; set; }
        public override string ToString() => $"System: {SystemName}, Status: {Status}, ExecutionTime: {WaitTime}ms";
    }
}
