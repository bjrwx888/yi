using Yi.BBS.Application.Contracts.GlobalSetting;
using NET.AutoWebApi.Setting;
using Yi.BBS.Domain.GlobalSetting.Entities;
using Yi.Framework.Ddd.Services;
using Microsoft.AspNetCore.Mvc;
using Yi.BBS.Application.Contracts.GlobalSetting.Dtos.Temp;
using Yi.BBS.Domain.Shared;

namespace Yi.BBS.Application.GlobalSetting
{
    /// <summary>
    ///临时服务，之后用其他模块代替
    /// </summary>
    [AppService]
    public class TempService : ApplicationService, IAutoApiService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Route("/api/account/login")]
        public Task<LoginDto> PostLoginAsync()
        {
            bool loginSucces = true;
            if (!loginSucces)
            {
                throw new UserFriendlyException("登录失败", (int)BbsHttpStatusEnum.LoginFailed, "用户或者密码错误");
            }
            var dto = new LoginDto("token");
            dto.User = new LoginUserInfoDto { Icon = "", Id = 0, Level = 1, UserName = "橙子" };
            return Task.FromResult(dto);
        }

        /// <summary>
        /// 判断是否有登录
        /// </summary>
        /// <returns></returns>
        [Route("/api/account/logged")]
        public Task<bool> PostLogged()
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [Route("/api/account/logout")]
        public Task PostlogOut()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/api/account/user/{id}")]
        public  Task<List<ActionJwtDto>> GetUserInfoByIdAsync(long id)
        {
            var dto = new List<ActionJwtDto>();
            dto.Add(new ActionJwtDto { Router = "/index", ActionName = "首页" });
            //dto.Add(new ActionJwtDto { Router = "", ActionName = "" });
            //dto.Add(new ActionJwtDto { Router = "", ActionName = "" });
            //dto.Add(new ActionJwtDto { Router = "", ActionName = "" });
            //dto.Add(new ActionJwtDto { Router = "", ActionName = "" });
            //dto.Add(new ActionJwtDto { Router = "", ActionName = "" });
            //dto.Add(new ActionJwtDto { Router = "", ActionName = "" });
            //dto.Add(new ActionJwtDto { Router = "", ActionName = "" });
            //dto.Add(new ActionJwtDto { Router = "", ActionName = "" });
            return Task.FromResult(dto);
        }
    }
}
