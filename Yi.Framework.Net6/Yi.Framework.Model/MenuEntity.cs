using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;
namespace Yi.Framework.Model.Models
{
    /// <summary>
    /// 菜单表
    ///</summary>
    public partial class MenuEntity
    {
        [SqlSugar.SugarColumn(IsIgnore = true)]
        public List<MenuEntity> Children { get; set; }
    }
}
