using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.BBS.Application.Contracts.Forum.Dtos;
using Yi.BBS.Application.Contracts.Forum.Dtos.Discuss;
using Yi.Framework.Ddd.Services.Abstract;

namespace Yi.BBS.Application.Contracts.Forum
{
    /// <summary>
    /// Discuss服务抽象
    /// </summary>
    public interface IDiscussService : ICrudAppService<DiscussGetOutputDto, DiscussGetListOutputDto, long, DiscussGetListInputVo, DiscussCreateInputVo, DiscussUpdateInputVo>
    {

    }
}
