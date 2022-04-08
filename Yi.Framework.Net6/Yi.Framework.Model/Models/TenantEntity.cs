using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 租户表
    ///</summary>
    [SugarTable("Tenant")]
    public partial class TenantEntity:BaseModelEntity
    {
        /// <summary>
        /// 租户名 
        ///</summary>
         [SugarColumn(ColumnName="TenantName"    )]
         public string TenantName { get; set; }
    }
}
