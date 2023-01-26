using Yi.BBS.Application.Contracts.Forum;
using NET.AutoWebApi.Setting;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Domain.Forum.Entities;
using Yi.Framework.Ddd.Services;

namespace Yi.BBS.Application.Forum
{
    /// <summary>
    /// Comment服务实现
    /// </summary>
    [AppService]
    public class CommentService : CrudAppService<CommentEntity, CommentGetOutputDto, CommentGetListOutputDto, long, CommentGetListInputVo, CommentCreateInputVo, CommentUpdateInputVo>,
       ICommentService, IAutoApiService
    {
    }
}
