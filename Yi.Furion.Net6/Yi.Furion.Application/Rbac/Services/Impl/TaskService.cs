using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.Schedule;
using Furion.TimeCrontab;
using Yi.Framework.Infrastructure.Ddd.Services;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    public class TaskService : ApplicationService, ITaskService, IDynamicApiController, ITransient
    {
        private readonly ISchedulerFactory _schedulerFactory;
        public TaskService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }
        public object GetById(string jobId)
        {
            var result = _schedulerFactory.TryGetJob(jobId, out var scheduler);
            return scheduler.GetModel();
        }
        public object Get()
        {
            return _schedulerFactory.GetJobsOfModels();
        }
        public object Create()
        {
            //jobBuilder
            var jobBuilder = JobBuilder.Create("YourProject", "YourProject.MyJob");

            //triggerBuilder
            //毫秒
            var triggerBuilder = Triggers.Period(5000);
            //cron
            var triggerBuilder2 = Triggers.Cron("* * * * *", CronStringFormat.Default);

            //作业计划,单个jobBuilder与多个triggerBuilder组合
            var schedulerBuilder = SchedulerBuilder.Create(jobBuilder, triggerBuilder, triggerBuilder2);


            //调度中心工厂，使用作业计划管理job,返回调度中心单个
            var result = _schedulerFactory.TryAddJob(schedulerBuilder, out var scheduler);

            return result;
        }
        public object Remove(string jobId)
        {
            var res = _schedulerFactory.TryRemoveJob(jobId, out var scheduler);
            return res;
        }
        public object Update()
        {
            //jobBuilder
            var jobBuilder = JobBuilder.Create("YourProject", "YourProject.MyJob");

            //triggerBuilder
            //毫秒
            var triggerBuilder = Triggers.Period(5000);
            //cron
            var triggerBuilder2 = Triggers.Cron("* * * * *", CronStringFormat.Default);

            //作业计划,单个jobBuilder与多个triggerBuilder组合
            var schedulerBuilder = SchedulerBuilder.Create(jobBuilder, triggerBuilder, triggerBuilder2);


            var result = _schedulerFactory.TryUpdateJob(schedulerBuilder, out var scheduler);
            return result;
        }
    }
}
