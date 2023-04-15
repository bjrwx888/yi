using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Rbac.Dtos.Post;

namespace Yi.Furion.Application.Rbac.Services
{
    /// <summary>
    /// Post服务抽象
    /// </summary>
    public interface IPostService : ICrudAppService<PostGetOutputDto, PostGetListOutputDto, long, PostGetListInputVo, PostCreateInputVo, PostUpdateInputVo>
    {

    }
}
