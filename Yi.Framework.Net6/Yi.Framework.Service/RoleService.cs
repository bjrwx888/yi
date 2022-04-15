using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class RoleService 
    {
        public async Task<List<RoleEntity>> DbTest()
        {
            return await _repository._Db.Queryable<RoleEntity>().ToListAsync();
        }
    }
}
