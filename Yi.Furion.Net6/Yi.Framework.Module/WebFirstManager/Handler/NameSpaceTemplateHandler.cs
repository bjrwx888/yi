using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    internal class NameSpaceTemplateHandler : TemplateHandlerBase, ITemplateHandler
    {
        public string Invoker(string str)
        {
            return str.Replace("@Namespace", "");
        }
    }
}
