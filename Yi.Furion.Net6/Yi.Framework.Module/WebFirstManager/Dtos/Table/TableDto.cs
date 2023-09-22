using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;
using Yi.Framework.Module.WebFirstManager.Entities;

namespace Yi.Framework.Module.WebFirstManager.Dtos.Table
{
    public class TableDto: IEntityDto<long>
    {
        public long Id { get; set; }
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
        public List<FieldEntity> Fields { get; set; }
    }
}
