using System.Xml.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Volo.Abp.Application.Services;
using Volo.Abp.Settings;
using Volo.Abp.Uow;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Banner;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Comment;
using Yi.Framework.Bbs.Application.Contracts.IServices;
using Yi.Framework.Bbs.Domain.Entities.Forum;
using Yi.Framework.Rbac.Domain.Authorization;
using Yi.Framework.Rbac.Domain.Extensions;
using Yi.Framework.SettingManagement.Domain;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Abp.Application.Services
{
    /// <summary>
    /// 常用魔改及扩展示例
    /// </summary>
    public class TestService : ApplicationService
    {
        [HttpGet("hello-world/string")]
        public async Task<string> GetHelloWorld1(string? name)
        {
            return "1";
        }
        [HttpGet("hello-world/dto")]
        public async Task<CommentGetOutputDto> GetHelloWorld2(string? name)
        {
            return new CommentGetOutputDto();
        }
    }
}
