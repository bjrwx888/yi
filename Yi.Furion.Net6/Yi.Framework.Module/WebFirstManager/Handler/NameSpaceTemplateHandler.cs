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
        public HandledTemplate Invoker(string str, string path)
        {
            var output = new HandledTemplate();
            output.TemplateStr = str.Replace("@namespace", "");
            output.BuildPath = path;
            return output;
        }
    }
}
