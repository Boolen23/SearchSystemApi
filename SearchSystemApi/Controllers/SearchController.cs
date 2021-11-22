using Microsoft.AspNetCore.Mvc;
using SearchSystemApi.Model;
using SearchSystemApi.Model.SearchSystem;
using SearchSystemApi.Model.Stat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSystemApi.Controllers
{
    public class SearchController : Controller
    {
        public SearchController(IStatisticsCollector StatisticsCollector)
        {
            this.StatisticsCollector = StatisticsCollector;
        }
        private IStatisticsCollector StatisticsCollector;

        [HttpGet("Metrics")]
        public IActionResult Metrics() => Json(StatisticsCollector.GetMetrics());

        [HttpGet("Search")]
        public async Task<IActionResult> Search(int Wait, int RandomMin, int RandomMax)
        {
            var a = ExternalSystem.A.Generate().Request(RandomMin, RandomMax);
            var b = ExternalSystem.B.Generate().Request(RandomMin, RandomMax);
            var c = ExternalSystem.C.Generate().Request(RandomMin, RandomMax);
            var d = ExternalSystem.D.Generate().Request(RandomMin, RandomMax);
            var waitTask = Wait.GetTimeoutTask();
            List<Task<(ExternalSystem system, bool IsOK, int ExecTime)>> taskList = new List<Task<(ExternalSystem system, bool IsOK, int ExecTime)>>();
            taskList.Add(a);
            taskList.Add(b);
            taskList.Add(c);
            taskList.Add(waitTask);
            while (taskList.Any())
            {
                var finishTask = await Task.WhenAny(taskList);
                taskList.Remove(finishTask);
                if(finishTask.Result.system == ExternalSystem.TimeoutTask)
                {
                    foreach (var tsk in taskList)
                    {
                        if (tsk == a) StatisticsCollector.Collect(ExternalSystem.A, ResponseStatus.TIMEOUT, Wait);
                        else if(tsk == b) StatisticsCollector.Collect(ExternalSystem.B, ResponseStatus.TIMEOUT, Wait);
                        else if (tsk == c) StatisticsCollector.Collect(ExternalSystem.C, ResponseStatus.TIMEOUT, Wait);
                        else if (tsk == d) StatisticsCollector.Collect(ExternalSystem.D, ResponseStatus.TIMEOUT, Wait);
                    }
                    break;
                }
                if (finishTask == c)
                    taskList.Add(d);
                StatisticsCollector.Collect(finishTask.Result.system, finishTask.Result.IsOK ? ResponseStatus.OK : ResponseStatus.ERROR, finishTask.Result.ExecTime);
            }
            return Json(StatisticsCollector.SearchResult());
        }
    }
}
