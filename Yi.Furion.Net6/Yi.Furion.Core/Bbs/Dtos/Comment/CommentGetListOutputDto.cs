using System;
using System.Collections.Generic;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Furion.Core.Rbac.Dtos.User;

namespace Yi.Furion.Core.Bbs.Dtos.Comment
{
    /// <summary>
    /// 评论多反
    /// </summary>
    public class CommentGetListOutputDto : IEntityDto<long>
    {
        public long Id { get; set; }


        public DateTime? CreationTime { get; set; }




        public string Content { get; set; }


        /// <summary>
        /// 主题id
        /// </summary>
        public long DiscussId { get; set; }

        public long ParentId { get; set; }

        public long RootId { get; set; }

        /// <summary>
        /// 用户,评论人用户信息
        /// </summary>
        public UserGetOutputDto CreateUser { get; set; }

        /// <summary>
        /// 被评论的用户信息
        /// </summary>
        public UserGetOutputDto CommentedUser { get; set; }


        /// <summary>
        /// 这个不是一个树形，而是存在一个二维数组，该Children只有在顶级时候，只有一层
        /// </summary>
        public List<CommentGetListOutputDto> Children { get; set; } = new List<CommentGetListOutputDto>();
    }
}
