using Yi.RBAC.Application.Contracts.Logs;
using NET.AutoWebApi.Setting;
using Yi.RBAC.Application.Contracts.Logs.Dtos;
using Yi.RBAC.Domain.Logs.Entities;
using Yi.Framework.Ddd.Services;
using Yi.Framework.Model.RABC.Entitys;
using Yi.Framework.Ddd.Dtos;

namespace Yi.RBAC.Application.Logs
{
    /// <summary>
    /// OperationLog服务实现
    /// </summary>
    [AppService]
    public class OperationLogService : CrudAppService<OperationLogEntity, OperationLogGetListOutputDto, long, OperationLogGetListInputVo >,
       IOperationLogService, IAutoApiService
    {
        public override Task<PagedResultDto<OperationLogGetListOutputDto>> GetListAsync(OperationLogGetListInputVo input)
        {
            return base.GetListAsync(input);
        }
    }
}
