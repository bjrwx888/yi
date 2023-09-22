using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Framework.Module.WebFirstManager.Dtos.Template
{
    public class TemplateDto : IEntityDto<long>
    {
        public long Id { get; set; }

        /// <summary>
        /// 模板字符串
        /// </summary>
        public string TemplateStr { get; set; } = string.Empty;

        /// <summary>
        /// 生成路径
        /// </summary>
        public string BuildPath { get; set; }
    }
}
