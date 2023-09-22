using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    public class NameSpaceTemplateHandler : TemplateHandlerBase, ITemplateHandler, ISingleton
    {
        public string Invoker(string str)
        {
            return str.Replace("@namespace", "");
        }
    }
}
