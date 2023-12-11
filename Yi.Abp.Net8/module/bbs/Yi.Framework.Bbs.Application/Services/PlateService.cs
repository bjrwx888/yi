using Volo.Abp.Domain.Repositories;
using Yi.Framework.Bbs.Application.Contracts.Dtos.Plate;
using Yi.Framework.Bbs.Application.Contracts.IServices;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Ddd.Application;

namespace Yi.Framework.Bbs.Application.Services
{
    /// <summary>
    /// Plate服务实现
    /// </summary>
    public class PlateService : YiCrudAppService<PlateEntity, PlateGetOutputDto, PlateGetListOutputDto, Guid, PlateGetListInputVo, PlateCreateInputVo, PlateUpdateInputVo>,
       IPlateService
    {
        public PlateService(IRepository<PlateEntity, Guid> repository) : base(repository)
        {
        }
    }
}
