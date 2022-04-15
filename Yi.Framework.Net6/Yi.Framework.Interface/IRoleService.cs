using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IRoleService
    {
        /// <summary>
        /// DbTest
        /// </summary>
        /// <returns></returns>
        public Task<List<RoleEntity>> DbTest();
    }
}
