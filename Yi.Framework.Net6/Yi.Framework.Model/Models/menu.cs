using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Models;

namespace Yi.Framework.Model.Models
{
    public class menu :loopModel<menu>,ITreeModel<menu>
    {
        public string icon { get; set; }
        public string router { get; set; }
        public string menu_name { get; set; }

        public List<role> roles { get; set; }
        public mould mould { get; set; }


    }
}
