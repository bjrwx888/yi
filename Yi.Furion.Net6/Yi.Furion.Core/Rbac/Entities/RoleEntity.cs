using System;
using System.Collections.Generic;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Furion.Core.Rbac.Enums;

namespace Yi.Furion.Core.Rbac.Entities
{
    /// <summary>
    /// 角色表
    /// </summary>
    [SugarTable("Role")]
    public class RoleEntity : IEntity<long>, ISoftDelete, IAuditedObject, IOrderNum, IState
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建者
        /// </summary>
        public long? CreatorId { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public long? LastModifierId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;


        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 角色编码 
        ///</summary>
        [SugarColumn(ColumnName = "RoleCode")]
        public string RoleCode { get; set; } = string.Empty;

        /// <summary>
        /// 描述 
        ///</summary>
        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// 角色数据范围 
        ///</summary>
        [SugarColumn(ColumnName = "DataScope")]
        public DataScopeEnum DataScope { get; set; } = DataScopeEnum.ALL;

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; } = true;


        [Navigate(typeof(RoleMenuEntity), nameof(RoleMenuEntity.RoleId), nameof(RoleMenuEntity.MenuId))]
        public List<MenuEntity>? Menus { get; set; }

        [Navigate(typeof(RoleDeptEntity), nameof(RoleDeptEntity.RoleId), nameof(RoleDeptEntity.DeptId))]
        public List<DeptEntity>? Depts { get; set; }
    }
}
