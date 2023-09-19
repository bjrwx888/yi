using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Infrastructure.Ddd.Dtos;
using Yi.Framework.Module.WebFirstManager.Enums;

namespace Yi.Framework.Module.WebFirstManager.Dtos.Field
{
    public class FieldGetListInput:PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string? Name { get; set; }

        public long? TableId { get; set; }
    }
}
