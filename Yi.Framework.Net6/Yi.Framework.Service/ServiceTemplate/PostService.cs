using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class PostService : BaseService<PostEntity>, IPostService
    {
        public PostService(IRepository<PostEntity> repository) : base(repository)
        {
        }
    }
}
