using System;
using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Rbac.Core.Entities
{
    /// <summary>
    /// 岗位表
    ///</summary>
    [SugarTable("Post")]
    public partial class PostEntity : IEntity<long>, ISoftDelete, IAuditedObject, IOrderNum, IState
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
        /// 状态
        /// </summary>
        public bool State { get; set; } = true;

        /// <summary>
        /// 岗位编码 
        ///</summary>
        [SugarColumn(ColumnName = "PostCode")]
        public string PostCode { get; set; } = string.Empty;
        /// <summary>
        /// 岗位名称 
        ///</summary>
        [SugarColumn(ColumnName = "PostName")]
        public string PostName { get; set; } = string.Empty;

        /// <summary>
        /// 描述 
        ///</summary>
        [SugarColumn(ColumnName = "Remark")]
        public string Remark { get; set; }
    }
}
