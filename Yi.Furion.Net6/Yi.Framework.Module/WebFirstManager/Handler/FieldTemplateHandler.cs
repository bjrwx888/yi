using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    internal class FieldTemplateHandler : TemplateHandlerBase, ITemplateHandler
    {
        public string Invoker(string str)
        {
            //从数据库中获取到全部字段，然后根据字段生成字符串，进行替换
            return str.Replace("@field", "");
        }


    }
}
