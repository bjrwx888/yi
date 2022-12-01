using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Model.Models
{
    public partial class CommentEntity
    {
        /// <summary>
        /// 被回复的用户信息
        ///</summary>
        [Navigate(NavigateType.OneToOne,nameof(UserId),nameof(UserEntity.Id))]
        public UserEntity? UserInfo { get; set; }

        /// <summary>
        /// 创建评论的用户信息 
        ///</summary>
        [Navigate(NavigateType.OneToOne, nameof(CreateUser), nameof(UserEntity.Id))]
        public UserEntity? CreateUserInfo { get; set; }
    }
}
