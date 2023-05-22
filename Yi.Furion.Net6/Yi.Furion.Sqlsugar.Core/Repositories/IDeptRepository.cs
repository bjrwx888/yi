using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Sqlsugar.Core.Repositories
{
    public interface IDeptRepository
    {
        Task<List<long>> GetChildListAsync(long deptId);
        Task<List<DeptEntity>> GetListRoleIdAsync([FromRoute] long roleId);
    }
}
