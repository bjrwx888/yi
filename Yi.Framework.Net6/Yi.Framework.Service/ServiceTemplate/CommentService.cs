using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class CommentService : BaseService<CommentEntity>, ICommentService
    {
        public CommentService(IRepository<CommentEntity> repository) : base(repository)
        {
        }
    }
}
