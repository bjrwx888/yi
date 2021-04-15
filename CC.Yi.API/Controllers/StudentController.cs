﻿using CC.Yi.Common;
using CC.Yi.Common.Cache;
using CC.Yi.IBLL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;//处理日志相关文件

        //private UserManager<result_user> _userManager;//处理用户相关逻辑：添加密码，修改密码，添加删除角色等等
        //private SignInManager<result_user> _signInManager;//处理注册登录的相关逻辑

        private IstudentBll _studentBll;
        public StudentController(ILogger<StudentController> logger, IstudentBll studentBll)
        {

            _logger = logger;
            _logger.LogInformation("现在你进入了StudentController控制器");
            _studentBll = studentBll;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }
        #region
        //关于身份认证配置使用：
        //在需要身份认证的控制器上打上 [Authorize] 特性标签
        #endregion
        //[HttpGet]
        //public async Task<IActionResult> IdentityTest()
        //{
        //    //用户登入
        //    var data = await _signInManager.PasswordSignInAsync("账号", "密码", false, false); //"是否记住密码","是否登入失败锁定用户"
        //    //用户登出
        //    await _signInManager.SignOutAsync();
        //    //创建用户
        //    var data2 = await _userManager.CreateAsync(new result_user { UserName="账户",Email="邮箱"},"密码");
        //    //获取用户
        //    var data3 = _userManager.Users;//这里可以使用Linq表达式Select
        //    return Ok();
        //}

        #region
        //下面，这里是操作reids
        #endregion
        [HttpGet]
        public Result GetReids()
        {
            var data = CacheHelper.CacheWriter.GetCache<string>("key01");
            return Result.Success(data);
        }


        #region
        //下面，经典的 增删改查 即为简易--Yi意框架
        //注意：请确保你的数据库中存在合理的数据
        #endregion
        [HttpGet]
        public async Task<Result> GetTest()//查
        {
            _logger.LogInformation("调用查方法");
            var data =await  _studentBll.GetAllEntities().ToListAsync();
            return Result.Success("查询成功").SetData(data);
        }
        [HttpGet]
        public Result AddTest()//增
        {
            _logger.LogInformation("调用增方法");
            List<student> students = new List<student>() {new student { name = "学生a" } ,new student { name="学生d"} };
            if (_studentBll.Add(students))
            {
                return Result.Success("增加成功");
            }
            else
            {
                return Result.Error("增加失败");
            }
           

        }
        [HttpGet]
        public Result RemoveTest()//删
        {
            _logger.LogInformation("调用删方法");
            if (_studentBll.Delete(u=>u.name=="学生a"))
            {
                return Result.Success("删除成功");
            }
            else
            {
                return Result.Error("删除失败");
            }     
        }
        [HttpGet]
        public Result UpdateTest()//改
        {
            _logger.LogInformation("调用改方法");
            if (_studentBll.Update(new student { id=2, name = "学生a" }, "name"))
            {
                return Result.Success("修改成功");
            }
            else
            {
                return Result.Error("修改失败");
            }

        }
    }
}
