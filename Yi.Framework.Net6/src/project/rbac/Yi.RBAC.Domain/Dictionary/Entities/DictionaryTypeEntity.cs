using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Data.Auditing;
using Yi.Framework.Data.Entities;
using Yi.Framework.Ddd.Entities;

namespace Yi.RBAC.Domain.Dictionary.Entities
{
    [SugarTable("DictionaryType")]
    public class DictionaryTypeEntity : AuditedObject,IEntity<long>, ISoftDelete, IOrderNum
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
        /// 排序
        /// </summary>
        public int OrderNum { get; set; } = 0;


        /// <summary>
        /// 状态
        /// </summary>
        public bool? State { get; set; } = true;

        /// <summary>
        /// 字典名称 
        ///</summary>
        [SugarColumn(ColumnName = "DictName")]
        public string DictName { get; set; } = string.Empty;
        /// <summary>
        /// 字典类型 
        ///</summary>
        [SugarColumn(ColumnName = "DictType")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 描述 
        ///</summary>
        [SugarColumn(ColumnName = "Remark")]
        public string? Remark { get; set; }
    }
}
