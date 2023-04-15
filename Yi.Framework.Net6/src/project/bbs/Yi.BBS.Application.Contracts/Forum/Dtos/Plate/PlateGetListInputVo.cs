using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Ddd.Dtos;

namespace Yi.BBS.Application.Contracts.Forum.Dtos.Plate
{
    public class PlateGetListInputVo : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }
        public string? Introduction { get; set; }
    }
}
