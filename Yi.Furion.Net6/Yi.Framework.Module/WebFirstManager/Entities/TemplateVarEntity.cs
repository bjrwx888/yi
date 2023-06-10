using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WebFirstManager.Entities
{
    public class TemplateVarEntity
    {
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
