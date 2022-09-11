using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.DTOModel;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;
using Yi.Framework.WebCore;
using Yi.Framework.WebCore.AttributeExtend;
using Yi.Framework.WebCore.AuthorizationPolicy;

namespace Yi.Framework.ApiMicroservice.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseSimpleRdController<UserEntity>
    {
        private IUserService _iUserService;
        public UserController(ILogger<UserEntity> logger, IUserService iUserService) : base(logger, iUserService)
        {
            _iUserService = iUserService;
        }

        /// <summary>
        /// 动态条件分页查询
        /// </summary>
        /// <param name="user"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> PageList([FromQuery] UserEntity user, [FromQuery] PageParModel page)
        {
            return Result.Success().SetData(await _iUserService.SelctPageList(user, page));
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> UpdateStatus(long userId, bool isDel)
        {
            return Result.Success().SetData(await _iUserService._repository.UpdateIgnoreNullAsync(new UserEntity() { Id = userId, IsDeleted = isDel }));

        }


        ///// <summary>
        ///// 添加用户，去重，密码加密
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //[Permission($"{nameof(UserEntity)}:add")]
        //[HttpPost]
        //public  async Task<Result> Add(UserEntity entity)
        //{
        //    if (!await _iUserService.Exist(entity.UserName))
        //    {
        //        entity.BuildPassword();
        //        return Result.Success().SetData(await _iUserService._repository.InsertReturnSnowflakeIdAsync(entity));
        //    }
        //    return Result.SuccessError("用户已存在");
        //}

        /// <summary>
        /// 给多用户设置多角色
        /// </summary>
        /// <param name="giveUserSetRoleDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> GiveUserSetRole(GiveUserSetRoleDto giveUserSetRoleDto)
        {
            return Result.Success().SetStatus(await _iUserService.GiveUserSetRole(giveUserSetRoleDto.UserIds, giveUserSetRoleDto.RoleIds));
        }


        /// <summary>
        /// 通过用户id得到用户信息关联部门、岗位、角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public override async Task<Result> GetById([FromRoute] long id)
        {
            return Result.Success().SetData(await _iUserService.GetInfoById(id));
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> Update(UserInfoDto userDto)
        {
            return Result.Success().SetStatus(await _iUserService.UpdateInfo(userDto));
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> Add(UserInfoDto userDto)
        { 
            return Result.Success().SetStatus(await _iUserService.AddInfo(userDto));
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> RestPassword(UserEntity user)
        {
            return Result.Success().SetStatus(await _iUserService.RestPassword(user.Id, user.Password));
        }
    }
}
