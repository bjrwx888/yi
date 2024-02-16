using Volo.Abp.Domain.Repositories;
using Yi.Framework.CodeGun.Application.Contracts.Dtos.Table;
using Yi.Framework.CodeGun.Application.Contracts.IServices;
using Yi.Framework.CodeGun.Domain.Entities;
using Yi.Framework.Ddd.Application;

namespace Yi.Framework.CodeGun.Application.Services
{
    public class TableService : YiCrudAppService<TableAggregateRoot, TableDto, Guid, TableGetListInput>, ITableService
    {
        public TableService(IRepository<TableAggregateRoot, Guid> repository) : base(repository)
        {
        }
    }
}
