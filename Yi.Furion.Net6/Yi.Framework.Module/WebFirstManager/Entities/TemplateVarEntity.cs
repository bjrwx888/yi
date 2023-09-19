using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Yi.Framework.Infrastructure.Ddd.Entities;

namespace Yi.Framework.Module.WebFirstManager.Entities
{
    public class TemplateVarEntity : IEntity<long>
    {
        [SugarColumn( IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string Name { get; set; }=string.Empty;

        /// <summary>
        /// 变量值
        /// </summary>
        public string Value { get; set; } = string.Empty;

    }
}
