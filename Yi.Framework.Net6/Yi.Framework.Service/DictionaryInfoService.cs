using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class DictionaryInfoService : BaseService<DictionaryInfoEntity>, IDictionaryInfoService
    {
        public async Task<PageModel<List<DictionaryInfoEntity>>> SelctPageList(DictionaryInfoEntity dicInfo, PageParModel page)
        {
            RefAsync<int> total = 0;
            var data = await _repository._DbQueryable
                    .WhereIF(!string.IsNullOrEmpty(dicInfo.DictLabel), u => u.DictLabel.Contains(dicInfo.DictLabel))
                     .WhereIF(!string.IsNullOrEmpty(dicInfo.DictType), u => u.DictType.Contains(dicInfo.DictType))
                     .Where(u => u.IsDeleted == false)
                    .OrderBy(u => u.OrderNum, OrderByType.Desc)
                    .ToPageListAsync(page.PageNum, page.PageSize, total);

            return new PageModel<List<DictionaryInfoEntity>>(data, total);
        }
    }
}
