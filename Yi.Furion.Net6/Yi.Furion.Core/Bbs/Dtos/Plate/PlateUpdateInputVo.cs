using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Bbs.Dtos.Plate
{
    public class PlateUpdateInputVo
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Introduction { get; set; }
    }
}
