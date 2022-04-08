using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class UserService : BaseService<UserEntity>, IUserService
    {
        public UserService(IRepository<UserEntity> repository) : base(repository)
        {
        }
    }
}
