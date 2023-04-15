using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Furion.Core.Rbac.Dtos.LoginLog;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Application.Rbac.Services.Impl
{
    public class LoginLogService : CrudAppService<LoginLogEntity, LoginLogGetListOutputDto, long, LoginLogGetListInputVo>,IDynamicApiController,ITransient
    {
        public override async Task<PagedResultDto<LoginLogGetListOutputDto>> GetListAsync(LoginLogGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.LoginIp), x => x.LoginIp.Contains(input.LoginIp!))
                          .WhereIF(!string.IsNullOrEmpty(input.LoginUser), x => x.LoginUser!.Contains(input.LoginUser!))
                          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<LoginLogGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }

        [NonAction]
        public override Task<LoginLogGetListOutputDto> UpdateAsync(long id, LoginLogGetListOutputDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}
