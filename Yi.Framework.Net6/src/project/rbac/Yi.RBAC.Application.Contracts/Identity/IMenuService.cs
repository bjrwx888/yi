using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Application.Contracts.Identity.Dtos;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.RBAC.Application.Contracts.Identity
{
    /// <summary>
    /// Menu服务抽象
    /// </summary>
    public interface IMenuService : ICrudAppService<MenuGetOutputDto, MenuGetListOutputDto, long, MenuGetListInputVo, MenuCreateInputVo, MenuUpdateInputVo>
    {

    }
}
