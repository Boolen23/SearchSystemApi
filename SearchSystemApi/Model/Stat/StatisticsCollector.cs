using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSystemApi.Model.Stat
{
    public class StatisticsCollector : IStatisticsCollector
    {
        public static List<ExecutionStatistic> GlobalStatistics = new List<ExecutionStatistic>();
        public List<ExecutionStatistic> LocalStatistics = new List<ExecutionStatistic>();
        public void Collect(ExternalSystem system, ResponseStatus status, int WaitTime)
        {
            var es = new ExecutionStatistic() { SystemName = system, Status = status, WaitTime = WaitTime };
            LocalStatistics.Add(es);
            GlobalStatistics.Add(es);
        }

        public IEnumerable<SystemExecutionMetric> GetMetrics()
        {
            return GlobalStatistics.GroupBy(i => new
            {
                Time = Convert.ToInt32(i.WaitTime / 1000f),
                System = i.SystemName
            }).Select(gr => new SystemExecutionMetric()
            {
                RequestCount = gr.Count(),
                SecondsWaitTime = gr.Key.Time,
                System = gr.Key.System
            });
        }

        public IEnumerable<string> SearchResult() => LocalStatistics.Select(i => i.ToString());
    }
}
