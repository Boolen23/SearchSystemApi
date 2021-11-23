using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSystemApi.Model.Stat
{
    public class StatisticsCollector : IStatisticsCollector
    {
        public static BlockingCollection<ExecutionStatistic> GlobalStatistics = new BlockingCollection<ExecutionStatistic>();
        public BlockingCollection<ExecutionStatistic> LocalStatistics = new BlockingCollection<ExecutionStatistic>();
        public void Collect(ExternalSystem system, ResponseStatus status, int WaitTime)
        {
            var es = new ExecutionStatistic() { SystemName = system, Status = status, WaitTime = WaitTime };
            LocalStatistics.Add(es);
            GlobalStatistics.Add(es);
        }

        public IEnumerable<string> GetMetrics()
        {
            return GlobalStatistics.GroupBy(i => new
            {
                Time = Convert.ToInt32(i.WaitTime / 1000f),
                System = i.SystemName.ToString()
            }).Select(gr => $"System: {gr.Key.System}, WaitTimeSeconds: {gr.Key.Time}, RequestCount: {gr.Count()}");
        }

        public IEnumerable<string> SearchResult() => LocalStatistics.Select(i => i.ToString());
    }
}
