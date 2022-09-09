using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 用户表
    ///</summary>
    [SugarTable("User")]
    public partial class UserEntity:IBaseModelEntity
    {
        public UserEntity()
        {
            this.IsDeleted = false;
            this.CreateTime = DateTime.Now;
        }
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true   )]
         public long Id { get; set; }
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
         [SugarColumn(ColumnName="CreateUser"    )]
         public long? CreateUser { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="CreateTime"    )]
         public DateTime? CreateTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="ModifyUser"    )]
         public long? ModifyUser { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="ModifyTime"    )]
         public DateTime? ModifyTime { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="IsDeleted"    )]
         public bool? IsDeleted { get; set; }
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
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Icon"    )]
         public string Icon { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Nick"    )]
         public string Nick { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Email"    )]
         public string Email { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Ip"    )]
         public string Ip { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Address"    )]
         public string Address { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Phone"    )]
         public string Phone { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="Introduction"    )]
         public string Introduction { get; set; }
        /// <summary>
        /// 排序字段 
        ///</summary>
         [SugarColumn(ColumnName="OrderNum"    )]
         public int? OrderNum { get; set; }
        /// <summary>
        /// 描述 
        ///</summary>
         [SugarColumn(ColumnName="Remark"    )]
         public string Remark { get; set; }
    }
}
