using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
using Yi.Framework.Ddd.Entities;

namespace Yi.RBAC.Domain.Identity.Entities;

    /// <summary>
    /// 角色部门关系表
    ///</summary>
    [SugarTable("RoleDept")]
public partial class RoleDeptEntity : IEntity<long>
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public long Id { get; set; }

    /// <summary>
    /// 角色id 
    ///</summary>
    [SugarColumn(ColumnName = "RoleId")]
    public long? RoleId { get; set; }
    /// <summary>
    /// 部门id 
    ///</summary>
    [SugarColumn(ColumnName = "DeptId")]
    public long? DeptId { get; set; }


}
