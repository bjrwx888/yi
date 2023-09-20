using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTool;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    internal class ModelTemplateHandler : TemplateHandlerBase, ITemplateHandler
    {
        public string Invoker(string str)
        {
            return str.Replace("@model", StrUtil.ToFirstLetterLowerCase(Table.Name)).Replace("@Model", StrUtil.ToFirstLetterUpperCase(Table.Name));
        }
    }
}
