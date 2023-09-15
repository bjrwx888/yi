using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;

namespace Yi.Framework.Module.OperLogManager
{
    /// <summary>
    /// OperationLog服务实现
    /// </summary>
    //[AppService]
    [ApiDescriptionSettings("OperLogManager")]
    public class OperationLogService : CrudAppService<OperationLogEntity, OperationLogGetListOutputDto, long, OperationLogGetListInputVo>,
       IOperationLogService, IDynamicApiController, ITransient
    {
        public override async Task<PagedResultDto<OperationLogGetListOutputDto>> GetListAsync(OperationLogGetListInputVo input)
        {
            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.OperUser), x => x.OperUser.Contains(input.OperUser!))
                          .WhereIF(input.OperType is not null, x => x.OperType == input.OperType)
                          .WhereIF(input.StartTime is not null && input.EndTime is not null, x => x.CreationTime >= input.StartTime && x.CreationTime <= input.EndTime)
                          .ToPageListAsync(input.PageNum, input.PageSize, total);
            return new PagedResultDto<OperationLogGetListOutputDto>(total, await MapToGetListOutputDtosAsync(entities));
        }

        [NonAction]
        public override Task<OperationLogGetListOutputDto> UpdateAsync(long id, OperationLogGetListOutputDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}
