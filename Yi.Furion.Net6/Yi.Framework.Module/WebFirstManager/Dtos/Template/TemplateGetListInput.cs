using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Framework.Module.WebFirstManager.Dtos.Template
{
    public class TemplateGetListInput : PagedAndSortedResultRequestDto
    {

        /// <summary>
        /// 模板名称
        /// </summary>
        public string? Name { get; set; }

    }
}
