using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Ddd.Dtos;
using Yi.RBAC.Application.Contracts.Identity.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    /// <summary>
    /// 评论多反
    /// </summary>
    public class CommentGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }

        public DateTime? CreateTime { get; set; }
        public string Content { get; set; }

        /// <summary>
        /// 根节点的评论id
        /// </summary>
        public long RootId { get; set; }

        /// <summary>
        /// 主题id
        /// </summary>
        public long DiscussId { get; set; }

        public long ParentId { get; set; }


        /// <summary>
        /// 用户,评论人用户信息，被评论的用户信息，是他的上一个节点评论
        /// </summary>
        public UserGetOutputDto User { get; set; }



        /// <summary>
        /// 这个不是一个树形，而是存在一个二维数组，该Children只有一层
        /// </summary>
        public List<CommentGetListOutputDto> Children { get; set; } = new List<CommentGetListOutputDto>();
    }
}
