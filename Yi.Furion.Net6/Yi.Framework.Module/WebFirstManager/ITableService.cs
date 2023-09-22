using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Framework.Module.WebFirstManager.Dtos.Table;

namespace Yi.Framework.Module.WebFirstManager
{
    public interface ITableService : ICrudAppService<TableDto, long, TableGetListInput>
    {
    }
}
