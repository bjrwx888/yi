using SqlSugar;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Yi.Framework.GoView.Domain.Shared.Enums;

namespace Yi.Framework.GoView.Domain.Entities
{
    /// <summary>
    /// GoView 项目表
    /// </summary>
    [SugarTable("GoViewProject", "GoView 项目表")]
    public class GoViewProjectEntity : FullAuditedEntity<long>
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public new long Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [SugarColumn(ColumnDescription = "项目名称", Length = 64)]
        [Required, MaxLength(64)]
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 项目状态
        /// </summary>
        [SugarColumn(ColumnDescription = "项目状态")]
        public GoViewProjectState State { get; set; } = GoViewProjectState.UnPublish;

        /// <summary>
        /// 预览图片Url
        /// </summary>
        [SugarColumn(ColumnDescription = "预览图片Url", Length = 1024)]
        [MaxLength(1024)]
        public string? IndexImage { get; set; }

        /// <summary>
        /// 项目备注
        /// </summary>
        [SugarColumn(ColumnDescription = "项目备注", Length = 512)]
        [MaxLength(512)]
        public string? Remarks { get; set; }
    }
}
