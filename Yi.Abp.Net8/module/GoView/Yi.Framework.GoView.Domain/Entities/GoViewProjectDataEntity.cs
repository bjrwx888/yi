using SqlSugar;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yi.Framework.GoView.Domain.Entities
{
    /// <summary>
    /// GoView 项目数据表
    /// </summary>
    [SugarTable("GoViewProjectData", "GoView 项目数据表")]
    public class GoViewProjectDataEntity : FullAuditedEntity<long>
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public new long Id { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        [SugarColumn(ColumnDescription = "项目内容", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string? Content { get; set; }

        /// <summary>
        /// 预览图片
        /// </summary>
        [SugarColumn(ColumnDescription = "预览图片", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string? IndexImageData { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        [SugarColumn(ColumnDescription = "背景图片", ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public string? BackGroundImageData { get; set; }
    }
}
