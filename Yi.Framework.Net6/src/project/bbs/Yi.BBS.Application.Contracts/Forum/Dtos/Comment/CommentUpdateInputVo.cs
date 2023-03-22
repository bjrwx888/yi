using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    public class CommentUpdateInputVo
    {

        public string Content { get; set; }

        //更新不能将评论转移
    }
}
