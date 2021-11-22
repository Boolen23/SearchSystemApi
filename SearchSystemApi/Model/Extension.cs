using SearchSystemApi.Model.SearchSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSystemApi.Model
{
    public static class Extension
    {
        public static SearchSystemBase Generate(this ExternalSystem system)
        {
            switch (system)
            {
                case ExternalSystem.A: return new ExternalA(system);
                case ExternalSystem.B: return new ExternalB(system);
                case ExternalSystem.C: return new ExternalC(system);
                case ExternalSystem.D: return new ExternalD(system);
                default: throw new Exception($"Unexpected system: {system}");
            }
        }
        public static async Task<(ExternalSystem system, bool IsOK, int ExecTime)> GetTimeoutTask(this int WaitMs)
        {
            await Task.Delay(WaitMs);
            return (ExternalSystem.TimeoutTask, false, WaitMs);
        }
    }
}
