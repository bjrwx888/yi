using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("Role")]
    public partial class RoleEntity:BaseModelEntity
    {
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="RoleName"    )]
         public string RoleName { get; set; }
        /// <summary>
        /// 租户Id 
        ///</summary>
         [SugarColumn(ColumnName="TenantId"    )]
         public long? TenantId { get; set; }
    }
}
