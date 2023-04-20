using SqlSugar;
using Yi.Framework.Infrastructure.Data.Auditing;
using Yi.Framework.Infrastructure.Data.Entities;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Furion.Core.Rbac.Entities
{
    /// <summary>
    /// 配置表
    /// </summary>
    [SugarTable("Config")]
    public class ConfigEntity : IEntity<long>, IAuditedObject, IOrderNum, ISoftDelete
    {
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 配置名称 
        ///</summary>
        [SugarColumn(ColumnName = "ConfigName")]
        public string ConfigName { get; set; } = string.Empty;
        /// <summary>
        /// 配置键 
        ///</summary>
        [SugarColumn(ColumnName = "ConfigKey")]
        public string ConfigKey { get; set; } = string.Empty;
        /// <summary>
        /// 配置值 
        ///</summary>
        [SugarColumn(ColumnName = "ConfigValue")]
        public string ConfigValue { get; set; } = string.Empty;
        /// <summary>
        /// 配置类别 
        ///</summary>
        [SugarColumn(ColumnName = "ConfigType")]
        public string? ConfigType { get; set; }


        /// <summary>
        /// 排序字段 
        ///</summary>
        [SugarColumn(ColumnName = "OrderNum")]
        public int OrderNum { get; set; }
        /// <summary>
        /// 描述 
        ///</summary>
        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreationTime { get; set; }

        public long? CreatorId { get; set; }

        public long? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}
