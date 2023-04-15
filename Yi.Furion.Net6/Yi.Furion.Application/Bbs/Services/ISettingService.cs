using Yi.Framework.Infrastructure.Ddd.Services.Abstract;

namespace Yi.Furion.Application.Bbs.Services
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
