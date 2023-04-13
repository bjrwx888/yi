using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Furion.Rbac.Application.System.Dtos.Post;
using Yi.Furion.Rbac.Core.Entities;

namespace Yi.Furion.Rbac.Application.System.Services.Impl
{
    /// <summary>
    /// Post服务实现
    /// </summary>
    public class PostService : CrudAppService<PostEntity, PostGetOutputDto, PostGetListOutputDto, long, PostGetListInputVo, PostCreateInputVo, PostUpdateInputVo>,
       IPostService, ITransient, IDynamicApiController
    {
        public override async Task<PagedResultDto<PostGetListOutputDto>> GetListAsync(PostGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.PostName), x => x.PostName.Contains(input.PostName!))
                        .WhereIF(input.State is not null, x => x.State == input.State)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<PostGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }
    }
}
