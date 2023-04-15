using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Furion.Core.Bbs.Dtos.MyType
{
    public class MyTypeUpdateInputVo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public long UserId { get; set; }
    }
}
