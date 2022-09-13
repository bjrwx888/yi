using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class UserPostService : BaseService<UserPostEntity>, IUserPostService
    {
        public UserPostService(IRepository<UserPostEntity> repository) : base(repository)
        {
        }
    }
}
