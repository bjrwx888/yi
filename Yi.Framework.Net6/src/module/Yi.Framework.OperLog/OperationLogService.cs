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
        public override Task<PagedResultDto<OperationLogGetListOutputDto>> GetListAsync(OperationLogGetListInputVo input)
        {
            return base.GetListAsync(input);
        }
    }
}
