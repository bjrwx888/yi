using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Furion.Core.Bbs.Dtos.Banner;
using Yi.Furion.Core.Bbs.Entities;

namespace Yi.Furion.Application.Bbs.Services.Impl
{
    /// <summary>
    /// Banner服务实现
    /// </summary>
    [ApiDescriptionSettings("BBS")]
    public class BannerService : CrudAppService<BannerEntity, BannerGetOutputDto, BannerGetListOutputDto, long, BannerGetListInputVo, BannerCreateInputVo, BannerUpdateInputVo>,
       IBannerService,IDynamicApiController,ITransient
    {
    }
}
