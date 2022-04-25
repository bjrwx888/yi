using System.Collections.Generic;
using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface IMenuService:IBaseService<MenuEntity>
    {
        Task<List<MenuEntity>> GetMenuTreeAsync();
    }
}
