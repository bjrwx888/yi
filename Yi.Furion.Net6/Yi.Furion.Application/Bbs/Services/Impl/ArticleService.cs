using Yi.Framework.Infrastructure.CurrentUsers;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Infrastructure.Exceptions;
using Yi.Furion.Core.Bbs.Consts;
using Yi.Furion.Core.Bbs.Dtos.Article;
using Yi.Furion.Core.Bbs.Entities;
using Yi.Furion.Core.Rbac.Consts;
using Yi.Furion.Sqlsugar.Core.Repositories;

namespace Yi.Furion.Application.Bbs.Services.Impl
{
    /// <summary>
    /// Article服务实现
    /// </summary>
    [ApiDescriptionSettings("BBS")]

    public class ArticleService : CrudAppService<ArticleEntity, ArticleGetOutputDto, ArticleGetListOutputDto, long, ArticleGetListInputVo, ArticleCreateInputVo, ArticleUpdateInputVo>,
       IArticleService, IDynamicApiController, ITransient
    {
        public ArticleService(IArticleRepository articleRepository,
            IRepository<DiscussEntity> discussRepository,
            ICurrentUser currentUser,
            IDiscussService discussService)
        {

            _articleRepository = articleRepository;
            _currentUser = currentUser;
            _discussRepository = discussRepository;
            _discussService = discussService;


        }
        private IArticleRepository _articleRepository { get; set; }
        private IRepository<DiscussEntity> _discussRepository { get; set; }
        private ICurrentUser _currentUser { get; set; }
        private IDiscussService _discussService { get; set; }
        /// <summary>
        /// 获取文章全部平铺信息
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Route("/api/article/all/discuss-id/{discussId}")]
        public async Task<List<ArticleAllOutputDto>> GetAllAsync([FromRoute] long discussId)
        {
            await _discussService.VerifyDiscussPermissionAsync(discussId);


            var entities = await _articleRepository.GetTreeAsync(x => x.DiscussId == discussId);
            //var result = entities.Tile();
            var items = _mapper.Map<List<ArticleAllOutputDto>>(entities);
            return items;
        }

        /// <summary>
        /// 查询文章
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<List<ArticleGetListOutputDto>> GetDiscussIdAsync([FromRoute] long discussId)
        {
            if (!await _discussRepository.IsAnyAsync(x => x.Id == discussId))
            {
                throw new UserFriendlyException(DiscussConst.主题不存在);
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
                throw new UserFriendlyException(DiscussConst.主题不存在);
            }
            if (input.ParentId != 0 && !await _repository.IsAnyAsync(x => x.Id == input.ParentId))
            {
                throw new UserFriendlyException(ArticleConst.文章不存在);
            }
            await VerifyDiscussCreateIdAsync(discuss.CreatorId);
            return await base.CreateAsync(input);
        }


        /// <summary>
        /// 效验创建权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task VerifyDiscussCreateIdAsync(long? userId)
        {
            //只有文章是特殊的，不能在其他主题下创建
            //主题的创建者不是当前用户，同时，没有权限或者超级管理

            //false  & true  & false  ,三个条件任意满意一个，即可成功使用||，最后取反，一个都不满足
            //
            if (userId != _currentUser.Id && !UserConst.Admin.Equals(_currentUser.UserName) && !_currentUser.Permission.Contains("bbs:discuss:add"))
            {
                throw new UserFriendlyException("无权限在其他用户主题中创建子文章");
            }
        }
    }
}
