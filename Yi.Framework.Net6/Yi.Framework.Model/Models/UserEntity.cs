using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 
    ///</summary>
    [SugarTable("User")]
    public partial class UserEntity:BaseModelEntity
    {
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Name"    )]
         public string Name { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Age"    )]
         public int? Age { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="TenantId"    )]
         public long? TenantId { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="UserName"    )]
         public string UserName { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Password"    )]
         public string Password { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Salt"    )]
         public string Salt { get; set; }
    }
}
