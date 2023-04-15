using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Application.Contracts.Exhibition.Dtos;
using Yi.BBS.Application.Contracts.Exhibition.Dtos.Banner;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.BBS.Application.Contracts.Exhibition
{
    /// <summary>
    /// Banner抽象
    /// </summary>
    public interface IBannerService : ICrudAppService<BannerGetOutputDto, BannerGetListOutputDto, long, BannerGetListInputVo, BannerCreateInputVo, BannerUpdateInputVo>
    {

    }
}
