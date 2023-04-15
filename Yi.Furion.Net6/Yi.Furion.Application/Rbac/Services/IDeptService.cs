using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Application.Rbac.Dtos.Dept;

namespace Yi.Furion.Application.Rbac.Services
{
    /// <summary>
    /// Dept服务抽象
    /// </summary>
    public interface IDeptService : ICrudAppService<DeptGetOutputDto, DeptGetListOutputDto, long, DeptGetListInputVo, DeptCreateInputVo, DeptUpdateInputVo>
    {

    }
}
