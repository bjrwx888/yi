using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.Schedule;

namespace Yi.Furion.Application.Rbac.Job
{
    public class TestJob : IJob
    {
        public Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
        {
            ddd.ttt += 1;
            Console.WriteLine($"你好，执行了{ddd.ttt}次");
            return Task.CompletedTask;
        }
    }

    public class ddd
    {
        public static int ttt = 0;
    }
}
