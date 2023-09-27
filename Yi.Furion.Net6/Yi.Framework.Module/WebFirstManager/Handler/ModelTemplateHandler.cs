using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTool;
using Furion.DependencyInjection;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    public class ModelTemplateHandler : TemplateHandlerBase, ITemplateHandler, ISingleton
    {
        public HandledTemplate Invoker(string str, string path)
        {
            var output = new HandledTemplate();
            output.TemplateStr= str.Replace("@model", StrUtil.ToFirstLetterLowerCase(Table.Name)).Replace("@Model", StrUtil.ToFirstLetterUpperCase(Table.Name));
            output.BuildPath = path.Replace("@model", StrUtil.ToFirstLetterLowerCase(Table.Name)).Replace("@Model", StrUtil.ToFirstLetterUpperCase(Table.Name));
            return output;
        }
    }
}
