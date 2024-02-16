using Yi.Framework.CodeGun.Application.Contracts.Dtos.Table;
using Yi.Framework.Ddd.Application.Contracts;

namespace Yi.Framework.CodeGun.Application.Contracts.IServices
{
    public interface ITableService : IYiCrudAppService<TableDto, Guid, TableGetListInput>
    {
    }
}
