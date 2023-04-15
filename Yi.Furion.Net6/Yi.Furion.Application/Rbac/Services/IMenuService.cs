using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Application.Rbac.Dtos.Menu;

namespace Yi.Furion.Application.Rbac.Services
{
    /// <summary>
    /// Menu服务抽象
    /// </summary>
    public interface IMenuService : ICrudAppService<MenuGetOutputDto, MenuGetListOutputDto, long, MenuGetListInputVo, MenuCreateInputVo, MenuUpdateInputVo>
    {

    }
}
