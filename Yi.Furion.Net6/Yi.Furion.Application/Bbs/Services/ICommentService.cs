using Yi.Framework.Infrastructure.Ddd.Services.Abstract;
using Yi.Furion.Core.Bbs.Dtos.Comment;

namespace Yi.Furion.Application.Bbs.Services
{
    /// <summary>
    /// Comment服务抽象
    /// </summary>
    public interface ICommentService : ICrudAppService<CommentGetOutputDto, CommentGetListOutputDto, long, CommentGetListInputVo, CommentCreateInputVo, CommentUpdateInputVo>
    {

    }
}
