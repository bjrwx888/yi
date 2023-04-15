using Yi.Framework.Infrastructure.Ddd.Services;

namespace Yi.Furion.Application.Bbs.Services.Impl
{
    /// <summary>
    /// Setting服务实现
    /// </summary>
    public class SettingService : ApplicationService,
       ISettingService,IDynamicApiController,ITransient
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
