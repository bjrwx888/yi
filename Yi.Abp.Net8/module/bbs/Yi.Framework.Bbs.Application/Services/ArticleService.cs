using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Article;
using Yi.Framework.Bbs.Application.Contracts.IServices;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Repositories;
using Yi.Framework.Bbs.Domain.Shared.Consts;
using Yi.Framework.Core.Extensions;
using Yi.Framework.Ddd.Application;
using Yi.Framework.Rbac.Domain.Shared.Consts;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Bbs.Application.Services
{
    /// <summary>
    /// Article服务实现
    /// </summary>

    public class ArticleService : YiCrudAppService<ArticleEntity, ArticleGetOutputDto, ArticleGetListOutputDto, Guid, ArticleGetListInputVo, ArticleCreateInputVo, ArticleUpdateInputVo>,
       IArticleService
    {
        public ArticleService(IArticleRepository articleRepository,
            ISqlSugarRepository<DiscussEntity> discussRepository,
            IDiscussService discussService) : base(articleRepository)
        {

            _articleRepository = articleRepository;
            _discussRepository = discussRepository;
            _discussService = discussService;


        }
        private IArticleRepository _articleRepository { get; set; }
        private ISqlSugarRepository<DiscussEntity> _discussRepository { get; set; }
        private IDiscussService _discussService { get; set; }
        /// <summary>
        /// 获取文章全部平铺信息
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Route("article/all/discuss-id/{discussId}")]
        public async Task<List<ArticleAllOutputDto>> GetAllAsync([FromRoute] Guid discussId)
        {
            await _discussService.VerifyDiscussPermissionAsync(discussId);


            var entities = await _articleRepository.GetTreeAsync(x => x.DiscussId == discussId);
            //var result = entities.Tile();
            var items = entities.Adapt<List<ArticleAllOutputDto>>();
            return items;
        }

        /// <summary>
        /// 查询文章
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<List<ArticleGetListOutputDto>> GetDiscussIdAsync([FromRoute] Guid discussId)
        {
            if (!await _discussRepository.IsAnyAsync(x => x.Id == discussId))
            {
                throw new UserFriendlyException(DiscussConst.No_Exist);
            }

            var entities = await _articleRepository.GetTreeAsync(x => x.DiscussId == discussId);
            var items = await MapToGetListOutputDtosAsync(entities);
            return items;
        }

        /// <summary>
        /// 发表文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async override Task<ArticleGetOutputDto> CreateAsync(ArticleCreateInputVo input)
        {
            var discuss = await _discussRepository.GetFirstAsync(x => x.Id == input.DiscussId);
            if (discuss is null)
            {
                throw new UserFriendlyException(DiscussConst.No_Exist);
            }
            if (input.ParentId != Guid.Empty && !await _articleRepository.IsAnyAsync(x => x.Id == input.ParentId))
            {
                throw new UserFriendlyException(ArticleConst.No_Exist);
            }
            await VerifyDiscussCreateIdAsync(discuss.CreatorId);
            return await base.CreateAsync(input);
        }


        /// <summary>
        /// 效验创建权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task VerifyDiscussCreateIdAsync(Guid? userId)
        {
            //只有文章是特殊的，不能在其他主题下创建
            //主题的创建者不是当前用户，同时，没有权限或者超级管理
            //false  & true  & false  ,三个条件任意满意一个，即可成功使用||，最后取反，一个都不满足
            //
            if (userId != CurrentUser.Id && !UserConst.Admin.Equals(this.CurrentUser.UserName) && this.LazyServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.GetUserPermissions(TokenTypeConst.Permission).Contains("bbs:discuss:add"))
            {
                throw new UserFriendlyException("无权限在其他用户主题中创建子文章");
            }
        }
    }
}
