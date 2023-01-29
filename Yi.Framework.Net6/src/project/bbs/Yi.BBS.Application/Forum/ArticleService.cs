using Yi.BBS.Application.Contracts.Forum;
using NET.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Ddd.Dtos;
using Microsoft.AspNetCore.Mvc;
using Yi.BBS.Domain.Forum.Repositories;
using Yi.Framework.Ddd.Repositories;
using Yi.BBS.Domain.Shared.Forum.ConstClasses;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// Article服务实现
    /// </summary>
    [AppService]

    public class ArticleService : CrudAppService<ArticleEntity, ArticleGetOutputDto, ArticleGetListOutputDto, long, ArticleGetListInputVo, ArticleCreateInputVo, ArticleUpdateInputVo>,
       IArticleService, IAutoApiService
    {
        [Autowired]
        private IArticleRepository _articleRepository { get; set; }


        [Autowired]
        private IRepository<DiscussEntity> _discussRepository { get; set; }

        /// <summary>
        /// 获取文章全部平铺信息
        /// </summary>
        /// <param name="discussId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [Route("/api/article/all/discuss-id/{discussId}")]
        public async Task<List<ArticleAllOutputDto>> GetAllAsync([FromRoute] long discussId)
        {
            if (!await _discussRepository.IsAnyAsync(x => x.Id == discussId))
            {
                throw new UserFriendlyException(DiscussConst.主题不存在);
            }

            var entities = await _articleRepository.GetTreeAsync(x => x.DiscussId == discussId);
            var result = entities.Tile();
            var items = _mapper.Map<List<ArticleAllOutputDto>>(result);
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
            if (!await _discussRepository.IsAnyAsync(x => x.Id == input.DiscussId))
            {
                throw new UserFriendlyException(DiscussConst.主题不存在);
            }
            if (input.ParentId != 0 && !await _repository.IsAnyAsync(x => x.Id == input.ParentId))
            {
                throw new UserFriendlyException(ArticleConst.文章不存在);
            }
            return await base.CreateAsync(input);
        }
    }
}
