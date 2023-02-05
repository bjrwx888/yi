using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Core.Sqlsugar.Repositories;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Dtos.Abstract;
using Yi.RBAC.Domain.Dictionary.Entities;
using Yi.RBAC.Domain.Dictionary.Repositories;
using Yi.RBAC.Domain.Identity.Entities;
using Yi.RBAC.Domain.Identity.Repositories;

namespace Yi.RBAC.Sqlsugar.Repositories
{
    [AppService]
    public class DictionaryRepository : SqlsugarRepository<DictionaryEntity>, IDictionaryRepository
    {
        public DictionaryRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<PagedDto<DictionaryEntity>> SelectGetListAsync(DictionaryEntity input, IPagedAndSortedResultRequestDto pageInput)
        {
            RefAsync<int> total = 0;
            var entities = await _DbQueryable.WhereIF(input.DictType is not null, x => x.DictType == input.DictType)
                      .WhereIF(input.DictLabel is not null, x => x.DictLabel!.Contains(input.DictLabel!))
                      .WhereIF(input.State is not null,x => x.State == input.State)
                      .ToPageListAsync(pageInput.PageNum, pageInput.PageSize, total);


            return new PagedDto<DictionaryEntity>(total, entities);
        }
    }
}
