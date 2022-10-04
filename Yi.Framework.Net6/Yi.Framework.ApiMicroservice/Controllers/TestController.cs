using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Const;
using Yi.Framework.Common.Models;
using Yi.Framework.Core;
using Yi.Framework.Interface;
using Yi.Framework.Language;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.AuthorizationPolicy;
using Yi.Framework.WebCore.DbExtend;
using Yi.Framework.WebCore.SignalRHub;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private IStringLocalizer<LocalLanguage> _local;
        private IUserService _iUserService;
        private IRoleService _iRoleService;
        private QuartzInvoker _quartzInvoker;
        private IHubContext<MainHub> _hub;
        //你可以依赖注入服务层各各接口，也可以注入其他仓储层，怎么爽怎么来！
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="hub"></param>
        /// <param name="logger"></param>
        /// <param name="iRoleService"></param>
        /// <param name="iUserService"></param>
        /// <param name="local"></param>
        /// <param name="quartzInvoker"></param>
        public TestController(IHubContext<MainHub> hub , ILogger<UserEntity> logger, IRoleService iRoleService, IUserService iUserService, IStringLocalizer<LocalLanguage> local, QuartzInvoker quartzInvoker)
        {
            _iUserService = iUserService;
            _iRoleService = iRoleService;
            _quartzInvoker = quartzInvoker;
            _hub = hub;
        }

        /// <summary>
        /// swagger跳转
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/")]
        public IActionResult Swagger()
        {
            return Redirect("/Swagger");
        }

        /// <summary>
        /// 仓储上下文对象测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // 特点：化繁为简！意框架仓储代理上下文对象，用起来就是爽，但最好按规范来爽！
        // 规范：控制器严禁使用DB上下文对象，其它怎么爽怎么来！
        public async Task<Result> DbTest()
        {
            //非常好，使用UserService的特有方法
            await _iUserService.DbTest();

            //非常好，依赖注入使用其他Service的特有方法
            await _iRoleService.DbTest();

            //很核理，使用仓储的通用方法
            await _iUserService._repository.GetListAsync();

            //挺不错，依赖注入其他仓储
            await _iRoleService._repository.GetListAsync();

            //还行，直接切换其他仓储，怎么爽怎么来
            await _iUserService._repository.ChangeRepository<Repository<RoleEntity>>().GetListAsync();

            //最好不要在控制器直接操作Db对象
            await _iUserService._repository._Db.Queryable<UserEntity>().ToListAsync();
            await _iUserService._repository._DbQueryable.ToListAsync();

            return Result.Success().SetData(await _iUserService.DbTest());
        }

        /// <summary>
        /// 执行Sql返回
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //简单语句不推荐使用sql！
        public async Task<Result> SqlTest()
        {
            return Result.Success().SetData(await _iUserService._repository.UseSqlAsync<UserEntity>("select * from User"));
        }

        /// <summary>
        /// 国际化测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //根据浏览器语言设置来切换输出
        public Result LocalTest()
        {
            return Result.Success().SetData(_local["succeed"]);
        }

        /// <summary>
        /// 权限测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Permission("user:get:test")]
        public Result PermissionTest()
        {
            return Result.Success();
        }

        /// <summary>
        /// 策略授权测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(PolicyName.Sid)]
        public Result AuthTest()
        {
            return Result.Success();
        }

        /// <summary>
        /// 异步事务测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //注册一个用户获取它的信息之后再更新它,但是这个年龄可能会报错
        //如果一个事务中有任何一个错误，将会把所有执行过的操作进行回滚，确保数据的原子性
        public async Task<Result> TranTest()
        {
            UserEntity user = new() { UserName = $"杰哥{DateTime.Now}", Password = "5201314", Age = 99 };

            var res = await _iUserService._repository.UseTranAsync(async () =>
            {
                await _iUserService.Register(user, (o) => user = o);
                user.Age = 18 / (new Random().Next(0, 2));
                await _iUserService._repository.UpdateAsync(user);
            });
            if (res)
            {
                return Result.Success("执行成功！");
            }
            else
            {
                return Result.Error("发生错误，插入已回滚！");
            }

        }

        /// <summary>
        /// 极爽导航属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //Sqlsugar精髓之一！必学！最新版本
        public async Task<Result> IncludeTest()
        {
            return Result.Success().SetData(await _iUserService.GetListInRole());
        }

        /// <summary>
        /// 启动一个定时任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //每5秒访问一次百度，可查看控制台
        public async Task<Result> JobTest()
        {
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {JobConst.method,"get" },
                {JobConst.url,"https://www.baidu.com" }
            };
            await _quartzInvoker.StartAsync("*/5 * * * * ?", "HttpJob", jobName: "test", jobGroup: "my", data: data);
            return Result.Success();
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> StopJob()
        {
            await _quartzInvoker.StopAsync(new Quartz.JobKey("test", "my"));
            return Result.Success();
        }

        /// <summary>
        /// 树形结构构建测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result TreeTest()
        {
            List<VueRouterModel> vueRouterModels = new()
            {
                new VueRouterModel { Id = 1, OrderNum = 1, ParentId = 0, Name = "001" },
                new VueRouterModel { Id = 2, OrderNum = 1, ParentId = 1, Name = "001001" },
                new VueRouterModel { Id = 3, OrderNum = 1, ParentId = 1, Name = "001002" }
            };
            var treeData = Common.Helper.TreeHelper.SetTree(vueRouterModels);
            return Result.Success().SetData(treeData);
        }

        /// <summary>
        /// 授权测试
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public Result AuthorizeTest()
        {
            return Result.Success();
        }


        /// <summary>
        /// 清空数据库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> ClearDb()
        {
            var rep = _iUserService._repository;
            return Result.Success().SetStatus(await rep.UseTranAsync(async () =>
            {
                await rep.DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<MenuEntity>>().DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<RoleEntity>>().DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<RoleMenuEntity>>().DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<UserRoleEntity>>().DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<DictionaryEntity>>().DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<DictionaryInfoEntity>>().DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<ConfigEntity>>().DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<PostEntity>>().DeleteAsync(u => true);
                await rep.ChangeRepository<Repository<DeptEntity>>().DeleteAsync(u => true);
            }));

        }


        /// <summary>
        /// 种子数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  Result SeedDb()
        {
            var rep = _iUserService._repository;
            return Result.Success().SetStatus(DbSeedExtend.Invoer(rep._Db));
        }

        /// <summary>
        /// 操作日志测试
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        [HttpPost]
        [Log("测试模块", Common.Enum.OperEnum.Insert)]
        public Result LogTest(List<string> par)
        {
            return Result.Success().SetData(par);
        }

        /// <summary>
        /// Signalr实时推送测试
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpGet]
        public async  Task<Result> SignalrTest(int msg)
        {
            await _hub.Clients.All.SendAsync("onlineNum", msg);
            return Result.Success("向所有连接客户端发送一个消息");
        }
        //job任务与公告管理
    }
}
