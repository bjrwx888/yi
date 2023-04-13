using Yi.Framework.Infrastructure.Ddd.Services.Abstract;

namespace Yi.Framework.Module.OperLogManager
{
    /// <summary>
    /// OperationLog服务抽象
    /// </summary>
    public interface IOperationLogService : ICrudAppService<OperationLogGetListOutputDto, long, OperationLogGetListInputVo>
    {

    }
}
