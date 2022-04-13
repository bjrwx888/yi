﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Language;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private IStringLocalizer<LocalLanguage> _local;
        private IUserService _iUserService;
        //你可以依赖注入服务层各各接口，也可以注入其他仓储层，怎么爽怎么来！
        public TestController(ILogger<UserEntity> logger, IUserService iUserService, IStringLocalizer<LocalLanguage> local)
        {
            _local = local;
            _iUserService = iUserService;
        }

        /// <summary>
        /// 仓储上下文对象测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // 特点：化繁为简！意框架仓储代理上下文对象，用起来就是爽，但最好按规范来爽！
        // 规范：控制器建议不要使用切换仓储方法，控制器严禁使用DB上下文对象，其它怎么爽怎么来！
        public async Task<Result> DbTest()
        {
            //非常好，使用UserService的特有方法
            await _iUserService.DbTest();

            //非常好，依赖注入使用其他Service的特有方法(就tm一张表，自己注入自己)
            await _iUserService.DbTest();

            //很核理，使用仓储的通用方法
            await _iUserService._repository.GetListAsync();

            //挺不错，依赖注入其他仓储(就tm一张表，自己注入自己)
            await _iUserService._repository.GetListAsync();

            //不建议，但爽了再说，直接切换其他仓储(就tm一张表，自己切换自己)
            await _iUserService._repository.ChangeRepository<Repository<UserEntity>>().GetListAsync();

            //恭喜你已经毕业了！此后将有一天，接手到这个的软件的程序员将破口大骂。
            await _iUserService._repository._Db.Queryable<UserEntity>().ToListAsync();

            return Result.Success().SetData(await _iUserService.DbTest());
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
        public Result AutnTest()
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

        //emmmm，看来一张表已经满足不了，接下来将要大更新一波
    }
}
