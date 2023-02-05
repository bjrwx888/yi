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

namespace Yi.RBAC.Sqlsugar.Repositories
{
    [AppService]
    public class DictionaryTypeRepository : SqlsugarRepository<DictionaryTypeEntity>, IDictionaryTypeRepository
    {
        public DictionaryTypeRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<PagedDto<DictionaryTypeEntity>> SelectGetListAsync(DictionaryTypeEntity input, IPagedAllResultRequestDto pageInput)
        {
            RefAsync<int> total = 0;
            var entities = await _DbQueryable.WhereIF(input.DictName is not null, x => x.DictName.Contains(input.DictName!))
                      .WhereIF(input.DictType is not null, x => x.DictType!.Contains(input.DictType!))
                      .WhereIF(input.State is not null, x => x.State == input.State)
                      .WhereIF(pageInput.StartTime is not null && pageInput.EndTime is not null ,x=>x.CreationTime>=pageInput.StartTime && x.CreationTime<=pageInput.EndTime)
                      .ToPageListAsync(pageInput.PageNum, pageInput.PageSize, total);


            return new PagedDto<DictionaryTypeEntity>(total, entities);
        }
    }
}
