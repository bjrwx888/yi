using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Rbac.Application.System.Dtos.Post;

namespace Yi.Furion.Rbac.Application.System.Services
{
    /// <summary>
    /// Post服务抽象
    /// </summary>
    public interface IPostService : ICrudAppService<PostGetOutputDto, PostGetListOutputDto, long, PostGetListInputVo, PostCreateInputVo, PostUpdateInputVo>
    {

    }
}
