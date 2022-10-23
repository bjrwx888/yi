using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 菜单表
    ///</summary>
    [SugarTable("Menu")]
    public partial class MenuEntity:IBaseModelEntity
    {
        public MenuEntity()
        {
            this.IsCache = false;
            this.CreateTime = DateTime.Now;
        }
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnName="Id" ,IsPrimaryKey = true   )]
         public long Id { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="MenuName"    )]
         public string? MenuName { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="MenuType"    )]
         public int? MenuType { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="PermissionCode"    )]
         public string? PermissionCode { get; set; }
        /// <summary>
        ///  
        ///</summary>
         [SugarColumn(ColumnName="ParentId"    )]
         public long? ParentId { get; set; }
        /// <summary>
        /// 创建者 
        ///</summary>
         [SugarColumn(ColumnName="CreateUser"    )]
         public long? CreateUser { get; set; }
        /// <summary>
        /// 创建时间 
        ///</summary>
         [SugarColumn(ColumnName="CreateTime"    )]
         public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改者 
        ///</summary>
         [SugarColumn(ColumnName="ModifyUser"    )]
         public long? ModifyUser { get; set; }
        /// <summary>
        /// 修改时间 
        ///</summary>
         [SugarColumn(ColumnName="ModifyTime"    )]
         public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 是否删除 
        ///</summary>
         [SugarColumn(ColumnName="IsDeleted"    )]
         public bool? IsDeleted { get; set; }
        /// <summary>
        /// 租户Id 
        ///</summary>
         [SugarColumn(ColumnName="TenantId"    )]
         public long? TenantId { get; set; }
        /// <summary>
        /// 菜单图标 
        ///</summary>
         [SugarColumn(ColumnName="MenuIcon"    )]
         public string? MenuIcon { get; set; }
        /// <summary>
        /// 菜单组件路由 
        ///</summary>
         [SugarColumn(ColumnName="Router"    )]
         public string? Router { get; set; }
        /// <summary>
        /// 是否为外部链接 
        ///</summary>
         [SugarColumn(ColumnName="IsLink"    )]
         public bool? IsLink { get; set; }
        /// <summary>
        /// 是否缓存 
        ///</summary>
         [SugarColumn(ColumnName="IsCache"    )]
         public bool? IsCache { get; set; }
        /// <summary>
        /// 是否显示 
        ///</summary>
         [SugarColumn(ColumnName="IsShow"    )]
         public bool? IsShow { get; set; }
        /// <summary>
        /// 排序字段 
        ///</summary>
         [SugarColumn(ColumnName="OrderNum"    )]
         public int? OrderNum { get; set; }
        /// <summary>
        /// 描述 
        ///</summary>
         [SugarColumn(ColumnName="Remark"    )]
         public string? Remark { get; set; }
        /// <summary>
        /// 组件路径 
        ///</summary>
         [SugarColumn(ColumnName="Component"    )]
         public string? Component { get; set; }
        /// <summary>
        /// 路由参数 
        ///</summary>
         [SugarColumn(ColumnName="Query"    )]
         public string? Query { get; set; }
    }
}
