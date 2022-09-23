using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class ConfigService : BaseService<ConfigEntity>, IConfigService
    {
        public async Task<PageModel<List<ConfigEntity>>> SelctPageList(ConfigEntity config, PageParModel page)
        {
            RefAsync<int> total = 0;
            var data = await _repository._DbQueryable
                    .WhereIF(!string.IsNullOrEmpty(config.ConfigName), u => u.ConfigName.Contains(config.ConfigName))
                    .WhereIF(!string.IsNullOrEmpty(config.ConfigKey), u => u.ConfigKey.Contains(config.ConfigKey))
                       .WhereIF(page.StartTime.IsNotNull() && page.EndTime.IsNotNull(), u => u.CreateTime >= page.StartTime && u.CreateTime <= page.EndTime)
                     .WhereIF(config.IsDeleted.IsNotNull(), u => u.IsDeleted == config.IsDeleted)
                    .OrderBy(u => u.OrderNum, OrderByType.Desc)
                          .ToPageListAsync(page.PageNum, page.PageSize, total);

            return new PageModel<List<ConfigEntity>>(data, total);
        }

    }
}
