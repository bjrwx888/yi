using SqlSugar;
using Yi.Framework.Interface;
using Yi.Framework.Model.Models;
using Yi.Framework.Repository;

namespace Yi.Framework.Service
{
    public partial class CategoryService : BaseService<CategoryEntity>, ICategoryService
    {
        public CategoryService(IRepository<CategoryEntity> repository) : base(repository)
        {
        }
    }
}
