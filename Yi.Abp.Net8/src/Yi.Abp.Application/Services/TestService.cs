using Volo.Abp.Application.Services;
using Volo.Abp.Uow;
using Yi.Framework.Bbs.Domain.Entities.Forum;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Abp.Application.Services
{
    /// <summary>
    /// 这是一个示例
    /// </summary>
    public class TestService : ApplicationService
    {
        public ISqlSugarRepository<BannerEntity> sqlSugarRepository { get; set; }
        /// <summary>
        /// 你好世界，动态Api
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetHelloWorld(string? name)
        {
            return name ?? "HelloWord";
        }

        /// <summary>
        /// 工作单元魔改
        /// 用户体验优先，万金油模式，支持多线程并发安全，支持多线程工作单元，支持无工作单元，支持。。。
        /// 自动在各个情况处理db客户端最优解之一
        /// </summary>
        /// <returns></returns>
        public async Task GetUowAsync()
        {
            int i = 10;
            List<Task> tasks = new List<Task>();

            while (i > 0)
            {
                tasks.Add(Task.Run(async () =>
                {
                    using (var uow = UnitOfWorkManager.Begin(true, true))
                    {
                        await sqlSugarRepository.InsertAsync(new BannerEntity { Name = "插入1" });
                        await uow.CompleteAsync();
                    }
                    await sqlSugarRepository.InsertAsync(new BannerEntity { Name = "插入2" });
                }));
                await sqlSugarRepository.InsertAsync(new BannerEntity { Name = "插入3" });
                i--;
            }

            await Task.WhenAll(tasks);
        }
    }
}
