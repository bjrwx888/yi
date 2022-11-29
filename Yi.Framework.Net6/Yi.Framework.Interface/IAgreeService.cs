using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
    public partial interface IAgreeService : IBaseService<AgreeEntity>
    {
        Task<bool> OperateAsync(long articleId, long userId);
    }
}
