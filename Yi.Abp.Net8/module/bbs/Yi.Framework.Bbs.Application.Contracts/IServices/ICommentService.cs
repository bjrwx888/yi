using Yi.Framework.Bbs.Application.Contracts.Dtos.Comment;
using Yi.Framework.Ddd.Application.Contracts;

namespace Yi.Framework.Bbs.Application.Contracts.IServices
{
    /// <summary>
    /// Comment服务抽象
    /// </summary>
    public interface ICommentService : IYiCrudAppService<CommentGetOutputDto, CommentGetListOutputDto, Guid, CommentGetListInputVo, CommentCreateInputVo, CommentUpdateInputVo>
    {
        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        // [Permission("bbs:comment:add")]
        // [Authorize]
        Task<CommentGetOutputDto> CreateAsync(CommentCreateInputVo input);
    }
}
