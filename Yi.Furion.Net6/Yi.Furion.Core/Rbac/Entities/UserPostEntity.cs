using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Rbac.Entities;
/// <summary>
/// 用户岗位表
///</summary>
[SugarTable("UserPost")]
public partial class UserPostEntity : IEntity<long>
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true)]
    public long Id { get; set; }
    /// <summary>
    /// 用户id
    /// </summary>
    [SugarColumn(ColumnName = "UserId")]
    public long UserId { get; set; }
    /// <summary>
    /// 岗位id 
    ///</summary>
    [SugarColumn(ColumnName = "PostId")]
    public long PostId { get; set; }

}
