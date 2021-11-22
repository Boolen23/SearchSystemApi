using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSystemApi.Model.SearchSystem
{
    public abstract class SearchSystemBase
    {
        public SearchSystemBase(ExternalSystem System)
        {
            this.System = System;
            rn = new Random();
        }
        private static Random rn;
        /// <summary>
        /// Имитирует запрос во внешнюю систему
        /// </summary>
        /// <param name="MinExecutionTime"></param>
        /// <param name="MaxExecutionTime"></param>
        /// <returns></returns>
        public async Task<(ExternalSystem system, bool IsOK, int ExecTime)> Request(int MinExecutionTime, int MaxExecutionTime)
        {
            int ExecutionTime = rn.Next(MinExecutionTime, MaxExecutionTime);
            await Task.Delay(ExecutionTime);
            return (System, ExecutionTime % 2 == 0, ExecutionTime);
        }
        public ExternalSystem System { get; set; }
    }
}
