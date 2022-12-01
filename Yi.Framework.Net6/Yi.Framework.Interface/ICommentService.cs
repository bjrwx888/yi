using System.Threading.Tasks;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Interface
{
   public partial interface ICommentService
    {
        Task<bool> AddAsync(CommentEntity comment);
    }
}
