using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.RBAC.Application.Contracts.Setting.Dtos;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.RBAC.Application.Contracts.Setting
{
    /// <summary>
    /// Config服务抽象
    /// </summary>
    public interface IConfigService : ICrudAppService<ConfigGetOutputDto, ConfigGetListOutputDto, long, ConfigGetListInputVo, ConfigCreateInputVo, ConfigUpdateInputVo>
    {

    }
}
