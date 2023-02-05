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
    public interface IDictionaryTypeRepository : IRepository<DictionaryTypeEntity>
    {
        Task<PagedDto<DictionaryTypeEntity>> SelectGetListAsync(DictionaryTypeEntity input, IPagedAllResultRequestDto pageInput);
    }
}
