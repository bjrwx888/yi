using Yi.BBS.Application.Contracts.Forum;
using NET.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Services;
using Yi.BBS.Application.Contracts.Forum.Dtos.Plate;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// Plate服务实现
    /// </summary>
    [AppService]
    public class PlateService : CrudAppService<PlateEntity, PlateGetOutputDto, PlateGetListOutputDto, long, PlateGetListInputVo, PlateCreateInputVo, PlateUpdateInputVo>,
       IPlateService, IAutoApiService
    {
    }
}
