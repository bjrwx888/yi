using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Application.Contracts.Logs.Dtos;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.RBAC.Application.Contracts.Logs
{
    /// <summary>
    /// OperationLog服务抽象
    /// </summary>
    public interface IOperationLogService : ICrudAppService<OperationLogGetListOutputDto, long, OperationLogGetListInputVo >
    {

    }
}
