using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Furion.Core.Bbs.Dtos.Plate;
using Yi.Furion.Core.Bbs.Entities;

namespace Yi.Furion.Application.Bbs.Services.Impl
{
    /// <summary>
    /// Plate服务实现
    /// </summary>
    [ApiDescriptionSettings("BBS")]
    public class PlateService : CrudAppService<PlateEntity, PlateGetOutputDto, PlateGetListOutputDto, long, PlateGetListInputVo, PlateCreateInputVo, PlateUpdateInputVo>,
       IPlateService,IDynamicApiController,ITransient
    {
    }
}
