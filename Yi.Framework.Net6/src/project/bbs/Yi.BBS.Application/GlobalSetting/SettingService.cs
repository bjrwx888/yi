using Yi.BBS.Application.Contracts.GlobalSetting;
using Cike.AutoWebApi.Setting;
using Yi.BBS.Domain.GlobalSetting.Entities;
using Yi.Framework.Ddd.Services;

namespace Yi.BBS.Application.GlobalSetting
{
    /// <summary>
    /// Setting服务实现
    /// </summary>
    [AppService]
    public class SettingService : ApplicationService,
       ISettingService, IAutoApiService
    {
        /// <summary>
        /// 获取配置标题
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> GetTitleAsync()
        {
            return Task.FromResult("你好世界");
        }
    }
}
