using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.WebFirstManager.Dtos.Field;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Impl
{
    /// <summary>
    /// 字段管理
    /// </summary>
    [ApiDescriptionSettings("WebFirstManager")]
    public class FieldService:CrudAppService<FieldEntity, FieldDto,long, FieldGetListInput> ,IFieldService,ITransient,IDynamicApiController
    {
    }
}
