using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Yi.Framework.Infrastructure.Ddd.Dtos;

namespace Yi.Furion.Core.Bbs.Dtos.MyType
{
    public class MyTypeGetListInputVo : PagedAndSortedResultRequestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public long UserId { get; set; }
    }
}
