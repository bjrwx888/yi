using Yi.BBS.Application.Contracts.Forum;
using NET.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Services;
using Yi.BBS.Application.Contracts.Forum.Dtos.Discuss;
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// Discuss服务实现
    /// </summary>
    [AppService]
    public class DiscussService : CrudAppService<DiscussEntity, DiscussGetOutputDto, DiscussGetListOutputDto, long, DiscussGetListInputVo, DiscussCreateInputVo, DiscussUpdateInputVo>,
       IDiscussService, IAutoApiService
    {
        public async Task<PagedResultDto<DiscussGetListOutputDto>> GetPlateIdAsync([FromRoute] long plateId, [FromQuery] DiscussGetListInputVo input)
        {
            var entities = await _repository.GetPageListAsync(x => x.PlateId == plateId, input);
            var items= await MapToGetListOutputDtosAsync(entities);
            var total = await _repository.CountAsync(x=>x.IsDeleted==false);
            return new PagedResultDto<DiscussGetListOutputDto>(total, items);
        }
    }
}
