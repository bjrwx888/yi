using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;
using Yi.Framework.Ddd.Dtos.Abstract;
using Yi.Framework.Ddd.Repositories;
using Yi.RBAC.Domain.Dictionary.Entities;
using Yi.RBAC.Domain.Identity.Entities;

namespace Yi.RBAC.Domain.Dictionary.Repositories
{
    public interface IDictionaryRepository : IRepository<DictionaryEntity>
    {
        Task<PagedDto<DictionaryEntity>> SelectGetListAsync(DictionaryEntity input, IPagedAndSortedResultRequestDto pageInput);
    }
}
