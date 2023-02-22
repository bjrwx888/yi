using NET.AutoWebApi.Setting;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Core.Attributes;

namespace Yi.Framework.OperLog
{
    /// <summary>
    /// OperationLog服务实现
    /// </summary>
    [AppService]
    public class OperationLogService : CrudAppService<OperationLogEntity, OperationLogGetListOutputDto, long, OperationLogGetListInputVo>,
       IOperationLogService, IAutoApiService
    {
        public override  async Task<PagedResultDto<OperationLogGetListOutputDto>> GetListAsync(OperationLogGetListInputVo input)
        {
            var entity = await MapToEntityAsync(input);

            RefAsync<int> total = 0;

            var entities = await _DbQueryable.WhereIF(!string.IsNullOrEmpty(input.OperUser), x => x.OperUser.Contains(input.OperUser!))
                          .WhereIF(input.OperType is not null, x => x.OperType==input.OperType)
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
