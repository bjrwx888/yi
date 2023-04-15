using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Bbs.Dtos.Banner;

namespace Yi.Furion.Application.Bbs.Services
{
    /// <summary>
    /// Banner抽象
    /// </summary>
    public interface IBannerService : ICrudAppService<BannerGetOutputDto, BannerGetListOutputDto, long, BannerGetListInputVo, BannerCreateInputVo, BannerUpdateInputVo>
    {

    }
}
