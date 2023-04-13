using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Rbac.Application.System.Dtos.Menu;

namespace Yi.Furion.Rbac.Application.System.Services
{
    /// <summary>
    /// Menu服务抽象
    /// </summary>
    public interface IMenuService : ICrudAppService<MenuGetOutputDto, MenuGetListOutputDto, long, MenuGetListInputVo, MenuCreateInputVo, MenuUpdateInputVo>
    {

    }
}
