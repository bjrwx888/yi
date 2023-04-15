using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Bbs.Dtos.Discuss;

namespace Yi.Furion.Application.Bbs.Services
{
    /// <summary>
    /// Discuss服务抽象
    /// </summary>
    public interface IDiscussService : ICrudAppService<DiscussGetOutputDto, DiscussGetListOutputDto, long, DiscussGetListInputVo, DiscussCreateInputVo, DiscussUpdateInputVo>
    {
        Task VerifyDiscussPermissionAsync(long discussId);
    }
}
