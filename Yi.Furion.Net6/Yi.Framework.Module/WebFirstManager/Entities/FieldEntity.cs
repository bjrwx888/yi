using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Framework.Module.WebFirstManager.Enums;

namespace Yi.Framework.Module.WebFirstManager.Entities
{
    [SugarTable("Field")]
    public class FieldEntity : IEntity<long>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }
        public string? Description { get; set; }

        public int OrderNum { get; set; }
        public int Length { get; set; }

        public FieldTypeEnum FieldType { get; set; }

        public long TableId { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsAutoAdd { get; set; }

        /// <summary>
        /// 是否公共
        /// </summary>
        public bool IsPublic { get; set; }
    }
}
