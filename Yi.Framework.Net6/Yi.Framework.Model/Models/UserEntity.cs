using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 用户表
    ///</summary>
    [SugarTable("User")]
    public partial class UserEntity:BaseModelEntity
    {
        /// <summary>
        /// 姓名 
        ///</summary>
         [SugarColumn(ColumnName="Name"    )]
         public string Name { get; set; }
        /// <summary>
        /// 年龄 
        ///</summary>
         [SugarColumn(ColumnName="Age"    )]
         public int? Age { get; set; }
        /// <summary>
        /// 租户Id 
        ///</summary>
         [SugarColumn(ColumnName="TenantId"    )]
         public Guid? TenantId { get; set; }
        /// <summary>
        /// 账户 
        ///</summary>
         [SugarColumn(ColumnName="UserName"    )]
         public string UserName { get; set; }
        /// <summary>
        /// 密码 
        ///</summary>
         [SugarColumn(ColumnName="Password"    )]
         public string Password { get; set; }
        /// <summary>
        /// 加密盐值 
        ///</summary>
         [SugarColumn(ColumnName="Salt"    )]
         public string Salt { get; set; }
    }
}
