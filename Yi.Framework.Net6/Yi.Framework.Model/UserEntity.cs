using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    public partial class UserEntity
    {
        /// <summary>
        ///  看好啦！ORM精髓，导航属性
        ///</summary>
        [Navigate(typeof(UserRoleEntity), nameof(UserRoleEntity.UserId), nameof(UserRoleEntity.RoleId))]
        public List<RoleEntity> Roles { get; set; }


        /// <summary>
        /// 构建密码，MD5盐值加密
        /// </summary>
        public void BuildPassword(string password = null)
        {
            //如果不传值，那就把自己的password当作传进来的password
            if (password == null)
            {
                password = this.Password;
            }
            this.Salt = Common.Helper.MD5Helper.GenerateSalt();
            this.Password = Common.Helper.MD5Helper.SHA2Encode(password, this.Salt);
        }
    }
}
