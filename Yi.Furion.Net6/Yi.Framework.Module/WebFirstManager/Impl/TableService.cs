using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.WebFirstManager.Dtos.Table;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Impl
{
    [ApiDescriptionSettings("WebFirstManager")]
    public class TableService : CrudAppService<TableAggregateRoot, TableDto, long, TableGetListInput> ,ITableService, ITransient, IDynamicApiController
    {
    }
}
