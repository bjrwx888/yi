using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Bbs.Dtos.Plate
{
    /// <summary>
    /// Plate输入创建对象
    /// </summary>
    public class PlateCreateInputVo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Introduction { get; set; }
    }
}
