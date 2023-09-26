using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyTool;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Infrastructure.Ddd.Services;
using Yi.Framework.Module.DictionaryManager.Dtos.DictionaryType;
using Yi.Framework.Module.WebFirstManager.Dtos.Field;
using Yi.Framework.Module.WebFirstManager.Entities;
using Yi.Framework.Module.WebFirstManager.Enums;

namespace Yi.Framework.Module.WebFirstManager.Impl
{
    /// <summary>
    /// 字段管理
    /// </summary>
    [ApiDescriptionSettings("WebFirstManager")]
    public class FieldService : CrudAppService<FieldEntity, FieldDto, long, FieldGetListInput>, IFieldService, ITransient, IDynamicApiController
    {
        public async override Task<PagedResultDto<FieldDto>> GetListAsync([FromQuery] FieldGetListInput input)
        {
            RefAsync<int> total = 0;
            var entities = await _DbQueryable.WhereIF(input.TableId is not null, x => x.TableId.Equals(input.TableId!))
                      .WhereIF(input.Name is not null, x => x.Name!.Contains(input.Name!))

                      .ToPageListAsync(input.PageNum, input.PageSize, total);

            return new PagedResultDto<FieldDto>
            {
                Total = total,
                Items = await MapToGetListOutputDtosAsync(entities)
            };
        }

        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <returns></returns>
        [Route("type")]
        public  object GetFieldTypeEnum()
        {
            return typeof(FieldTypeEnum).GetFields(BindingFlags.Static | BindingFlags.Public).Select(x => new { lable = x.Name, value = (int)EnumUtil.GetValueByName<FieldTypeEnum>(x.Name) }).ToList();
        }
    }
}
