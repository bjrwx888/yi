using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Framework.Module.WebFirstManager.Entities
{
    public class TableEntity : IEntity<long>
    {
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true)]
        public long Id { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Description { get; set; }

    }
}
