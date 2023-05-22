using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using Yi.Framework.Infrastructure.Sqlsugar.Repositories;
using Yi.Furion.Core.Bbs.Entities;
using Yi.Furion.Core.Rbac.Dtos.Dept;
using Yi.Furion.Core.Rbac.Entities;

namespace Yi.Furion.Sqlsugar.Core.Repositories.Impl
{
    public class DeptRepository : SqlsugarRepository<DeptEntity>, IDeptRepository, ITransient
    {
        public DeptRepository(ISqlSugarClient context) : base(context)
        {
        }

        public async Task<List<long>> GetChildListAsync(long deptId)
        {
            var entities = await _DbQueryable.ToChildListAsync(x => x.ParentId, deptId);
            return entities.Select(x => x.Id).ToList();
        }
        public async Task<List<DeptEntity>> GetListRoleIdAsync([FromRoute] long roleId)
        { 
        
        return await _DbQueryable.Where(d => SqlFunc.Subqueryable<RoleDeptEntity>().Where(rd => rd.RoleId == roleId && d.Id == rd.DeptId).Any()).ToListAsync();
        }
    }
}
