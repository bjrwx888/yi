using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Entities;
using Yi.Framework.Infrastructure.Helper;

namespace Yi.Framework.Module.WebFirstManager.Entities
{
    [SugarTable("Table")]
    public class TableAggregateRoot : IAggregateRoot<long>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; } = SnowflakeHelper.NextId;
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>

        public string? Description { get; set; }

        /// <summary>
        /// 一表多字段
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(FieldEntity.TableId))]
        public List<FieldEntity> Fields { get; set; }
    }
}
