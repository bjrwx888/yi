using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class SkuService : BaseService<SkuEntity>, ISkuService
    {
        public async Task<PageModel<List<SkuEntity>>> SelctPageList(SkuEntity enetity, PageParModel page)
        {
            RefAsync<int> total = 0;
            var data = await _repository._DbQueryable

                    .WhereIF(page.StartTime is not null && page.EndTime is not null, u => u.CreateTime >= page.StartTime && u.CreateTime <= page.EndTime)
                     .WhereIF(enetity.IsDeleted is not null, u => u.IsDeleted == enetity.IsDeleted)
                    .OrderBy(u => u.CreateTime, OrderByType.Desc)
                    .ToPageListAsync(page.PageNum, page.PageSize, total);
            return new PageModel<List<SkuEntity>>(data, total);
        }
    }
}
