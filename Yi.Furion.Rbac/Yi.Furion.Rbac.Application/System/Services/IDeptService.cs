using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Rbac.Application.System.Dtos.Dept;

namespace Yi.Furion.Rbac.Application.System.Services
{
    /// <summary>
    /// Dept服务抽象
    /// </summary>
    public interface IDeptService : ICrudAppService<DeptGetOutputDto, DeptGetListOutputDto, long, DeptGetListInputVo, DeptCreateInputVo, DeptUpdateInputVo>
    {

    }
}
