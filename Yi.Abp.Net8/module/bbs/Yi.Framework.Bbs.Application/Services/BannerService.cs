using Volo.Abp.Domain.Repositories;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Banner;
using Yi.Framework.Bbs.Application.Contracts.IServices;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Ddd.Application;

namespace Yi.Framework.Bbs.Application.Services
{
    /// <summary>
    /// Banner服务实现
    /// </summary>
    public class BannerService : YiCrudAppService<BannerEntity, BannerGetOutputDto, BannerGetListOutputDto, Guid, BannerGetListInputVo, BannerCreateInputVo, BannerUpdateInputVo>,
       IBannerService
    {
        public BannerService(IRepository<BannerEntity, Guid> repository) : base(repository)
        {
        }
    }
}
