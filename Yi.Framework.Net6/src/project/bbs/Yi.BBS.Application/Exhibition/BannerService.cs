using Yi.BBS.Application.Contracts.Exhibition;
using Cike.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Exhibition.Dtos;
using Yi.BBS.Domain.Exhibition.Entities;
using Yi.Framework.Ddd.Services;
using Yi.BBS.Application.Contracts.Exhibition.Dtos.Banner;

namespace Yi.BBS.Application.Exhibition
{
    /// <summary>
    /// Banner服务实现
    /// </summary>
    [AppService]
    public class BannerService : CrudAppService<BannerEntity, BannerGetOutputDto, BannerGetListOutputDto, long, BannerGetListInputVo, BannerCreateInputVo, BannerUpdateInputVo>,
       IBannerService, IAutoApiService
    {
    }
}
