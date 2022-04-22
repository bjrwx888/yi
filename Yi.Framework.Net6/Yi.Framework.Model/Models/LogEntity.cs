using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 日志表
    ///</summary>
    public partial class LogEntity:BaseModelEntity
    {
        /// <summary>
        /// 租户Id 
        ///</summary>
         [SugarColumn(ColumnName="TenantId"    )]
         public long? TenantId { get; set; }
        /// <summary>
        /// 日志信息 
        ///</summary>
         [SugarColumn(ColumnName="Message"    )]
         public string Message { get; set; }
    }
}
