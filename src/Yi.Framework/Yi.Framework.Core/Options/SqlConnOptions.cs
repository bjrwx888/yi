using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Core.Options
{
    public class SqlConnOptions
    {
        public string WriteUrl { get; set; } = string.Empty;
        public List<string>? ReadUrl { get; set; }
    }
}
