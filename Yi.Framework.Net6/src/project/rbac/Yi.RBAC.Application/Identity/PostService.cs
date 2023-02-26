using Yi.RBAC.Application.Contracts.Identity;
using Cike.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Ddd.Dtos;
using SqlSugar;
using Yi.RBAC.Application.Contracts.Setting.Dtos;

namespace Yi.RBAC.Application.Identity
{
    /// <summary>
    /// Post服务实现
    /// </summary>
    [AppService]
    public class PostService : CrudAppService<PostEntity, PostGetOutputDto, PostGetListOutputDto, long, PostGetListInputVo, PostCreateInputVo, PostUpdateInputVo>,
       IPostService, IAutoApiService
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
