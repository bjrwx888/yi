using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Common.Models
{

    public class PageModel<T>
    {
        public int Total { get; set; }
        public T Data { get; set; }
    }

    public class PageModel : PageModel<object>
    {
    }
}
