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
    public partial class DeptService : BaseService<DeptEntity>, IDeptService
    {
        public async Task<PageModel<List<DeptEntity>>> SelctPageList(DeptEntity dept, PageParModel page)
        {
            RefAsync<int> total = 0;
            var data = await _repository._DbQueryable
                    .WhereIF(!string.IsNullOrEmpty(dept.DeptName), u => u.DeptName.Contains(dept.DeptName))
                     .WhereIF(dept.IsDeleted.IsNotNull(), u => u.IsDeleted == dept.IsDeleted)
                    .OrderBy(u => u.OrderNum, OrderByType.Desc)
                    .ToPageListAsync(page.PageNum, page.PageSize, total);

            return new PageModel<List<DeptEntity>>(data, total);
        }
    }
}
