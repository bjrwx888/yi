using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Rbac.Entities;
/// <summary>
/// 角色菜单关系表
///</summary>
[SugarTable("RoleMenu")]
public partial class RoleMenuEntity : IEntity<long>

{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public long Id { get; set; }
    /// <summary>
    ///  
    ///</summary>
    [SugarColumn(ColumnName = "RoleId")]
    public long RoleId { get; set; }
    /// <summary>
    ///  
    ///</summary>
    [SugarColumn(ColumnName = "MenuId")]
    public long MenuId { get; set; }

}
