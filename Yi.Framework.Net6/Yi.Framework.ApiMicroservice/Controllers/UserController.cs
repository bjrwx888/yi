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
    public class UserController : BaseCrudController<UserEntity>
    {
        private IUserService _iUserService;
        public UserController(ILogger<UserEntity> logger, IUserService iUserService) : base(logger, iUserService)
        {
            _iUserService = iUserService;
        }

        [HttpGet]
        public async Task<Result> PageList()
        {
            return Result.Success().SetData(await _iUserService._repository.GetListAsync());
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isDel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> UpdateStatus(long userId,bool isDel)
        {
            return Result.Success().SetData(await _iUserService._repository.UpdateIgnoreNullAsync(new UserEntity() { Id = userId, IsDeleted = isDel }));
        
        }


        /// <summary>
        /// 添加用户，去重，密码加密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [Permission($"{nameof(UserEntity)}:add")]
        [HttpPost]
        public  override async Task<Result> Add(UserEntity entity)
        {
            if (!await _iUserService.Exist(entity.UserName))
            {
                entity.BuildPassword();
                return Result.Success().SetData(await _iUserService._repository.InsertReturnSnowflakeIdAsync(entity));
            }
            return Result.SuccessError("用户已存在");
        }

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
        /// 通过用户id得到角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetRoleListByUserId(long userId)
        {
            return Result.Success().SetData(await _iUserService.GetRoleListByUserId(userId));
        }
    }
}
