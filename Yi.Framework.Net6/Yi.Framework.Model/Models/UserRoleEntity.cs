using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("UserRole")]
    public partial class UserRoleEntity:BaseModelEntity
    {
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="RoleId"    )]
         public long? RoleId { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="UserId"    )]
         public long? UserId { get; set; }
        /// <summary>
        /// 租户Id 
        ///</summary>
         [SugarColumn(ColumnName="TenantId"    )]
         public long? TenantId { get; set; }
    }
}
