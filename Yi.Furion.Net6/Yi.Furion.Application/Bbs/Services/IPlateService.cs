using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Bbs.Dtos.Plate;

namespace Yi.Furion.Application.Bbs.Services
{
    /// <summary>
    /// Plate服务抽象
    /// </summary>
    public interface IPlateService : ICrudAppService<PlateGetOutputDto, PlateGetListOutputDto, long, PlateGetListInputVo, PlateCreateInputVo, PlateUpdateInputVo>
    {

    }
}
