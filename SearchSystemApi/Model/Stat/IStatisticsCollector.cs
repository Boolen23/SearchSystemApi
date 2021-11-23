using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSystemApi.Model.Stat
{
    public interface IStatisticsCollector
    {
        void Collect(ExternalSystem system, ResponseStatus status, int WaitTime);
        IEnumerable<string> GetMetrics();
        IEnumerable<string> SearchResult();
    }
}
