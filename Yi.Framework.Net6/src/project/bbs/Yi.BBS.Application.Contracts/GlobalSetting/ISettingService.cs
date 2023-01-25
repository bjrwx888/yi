using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.BBS.Application.Contracts.GlobalSetting
{
    /// <summary>
    /// Setting应用抽象
    /// </summary>
    public interface ISettingService : IApplicationService
    {
        /// <summary>
        /// 获取配置标题
        /// </summary>
        /// <returns></returns>
         Task<string> GetTitleAsync();
    }
}
