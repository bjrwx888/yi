using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.WebFirstManager.Dtos.Table;
using Yi.Framework.Module.WebFirstManager.Dtos.Template;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Impl
{
    [ApiDescriptionSettings("WebFirstManager")]
    public class TableService : CrudAppService<TableEntity, TableDto, long, TableGetListInput> ,ITableService, ITransient, IDynamicApiController
    {
    }
}
