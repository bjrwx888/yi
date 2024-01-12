using System.Reflection;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl.Matchers;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Yi.Framework.Rbac.Application.Contracts.Dtos.Task;
using Yi.Framework.Rbac.Application.Contracts.IServices;
using Yi.Framework.Rbac.Domain.Shared.Enums;

namespace Yi.Framework.Rbac.Application.Services
{
    public class TaskService : ApplicationService, ITaskService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        public TaskService(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }


        /// <summary>
        /// 单查job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("{jobId}")]
        public async Task<TaskGetOutput> GetByIdAsync([FromRoute] string jobId)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var jobDetail = await scheduler.GetJobDetail(new JobKey(jobId));
            var trigger = (await scheduler.GetTriggersOfJob(new JobKey(jobId))).First();
            //状态
            var state = await scheduler.GetTriggerState(trigger.Key);
            var output = new TaskGetOutput
            {
                JobId = jobDetail.Key.Name,
                GroupName = jobDetail.Key.Group,
                JobType = jobDetail.JobType.Name,
                Properties = Newtonsoft.Json.JsonConvert.SerializeObject(jobDetail.JobDataMap),
                Concurrent = !jobDetail.ConcurrentExecutionDisallowed,
                Description = jobDetail.Description,
                LastRunTime = trigger.GetPreviousFireTimeUtc()?.DateTime,
            };

            if (trigger is ISimpleTrigger simple)
            {
                output.TriggerArgs = simple.RepeatInterval.TotalMilliseconds.ToString() + "毫秒";

            }
            else if (trigger is ICronTrigger cron)
            {
                output.TriggerArgs = cron.CronExpressionString!;
            }
            return output;
        }

        /// <summary>
        /// 多查job
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<PagedResultDto<TaskGetListOutput>> GetList([FromQuery] TaskGetListInput input)
        {
            var items = new List<TaskGetOutput>();

            var scheduler = await _schedulerFactory.GetScheduler();

            var groups = await scheduler.GetJobGroupNames();

            foreach (var groupName in groups)
            {
                foreach (var jobKey in await scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName)))
                {
                    string jobName = jobKey.Name;
                    string jobGroup = jobKey.Group;
                    var triggers = (await scheduler.GetTriggersOfJob(jobKey)).First();
                    items.Add(await GetByIdAsync(jobName));
                }
            }


            var output = items.Skip((input.SkipCount - 1) * input.MaxResultCount).Take(input.MaxResultCount)
                .OrderByDescending(x => x.LastRunTime)
                .ToList();
            return new PagedResultDto<TaskGetListOutput>(items.Count(), output.Adapt<List<TaskGetListOutput>>());
        }

        /// <summary>
        /// 创建job
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Create(TaskCreateInput input)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            //设置启动时执行一次，然后最大只执行一次


            //jobBuilder
            var jobClassType = Assembly.LoadFrom(input.AssemblyName).GetType(input.JobType);

            var jobBuilder = JobBuilder.Create(jobClassType).WithIdentity(new JobKey(input.JobId, input.GroupName))
                .WithDescription(input.Description);
            if (!input.Concurrent)
            {
                jobBuilder.DisallowConcurrentExecution();
            }

            //triggerBuilder
            TriggerBuilder triggerBuilder = null;
            switch (input.Type)
            {
                case JobTypeEnum.Cron:
                    triggerBuilder = 
                       TriggerBuilder.Create().StartNow()
                       .WithCronSchedule(input.Cron);




                    break;
                case JobTypeEnum.Millisecond:
                    triggerBuilder =
                     TriggerBuilder.Create().StartNow()
                                            .WithSimpleSchedule(x => x
                                            .WithInterval(TimeSpan.FromMilliseconds(input.Millisecond))
                                            .RepeatForever()
                                            );
                    break;
            }

            //作业计划,单个jobBuilder与多个triggerBuilder组合
            await scheduler.ScheduleJob(jobBuilder.Build(), triggerBuilder.Build());
        }

        /// <summary>
        /// 移除job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public async Task Remove(string jobId)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.ResumeJob(new JobKey(jobId));
        }

        /// <summary>
        /// 暂停job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task Pause(string jobId)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.PauseJob(new JobKey(jobId));
        }

        /// <summary>
        /// 开始job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task Start(string jobId)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.ResumeJob(new JobKey(jobId));
        }

        /// <summary>
        /// 更新job
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(string jobId, TaskUpdateInput input)
        {
            await Remove(jobId);
            await Create(input.Adapt<TaskCreateInput>());
        }

        [HttpPost]
        public async Task RunOnce(string jobId)
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            var jobDetail = await scheduler.GetJobDetail(new JobKey(jobId));

            //设置启动时执行一次，然后最大只执行一次
            var trigger = TriggerBuilder.Create().WithIdentity(Guid.NewGuid().ToString()).StartNow()
                .WithSimpleSchedule(x => x
                .WithRepeatCount(1))
              .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
