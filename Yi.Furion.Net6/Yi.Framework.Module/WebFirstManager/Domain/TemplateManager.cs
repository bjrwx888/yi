using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Yi.Framework.Module.WebFirstManager.Handler;

namespace Yi.Framework.Module.WebFirstManager.Domain
{
    /// <summary>
    /// 模板领域服务
    /// </summary>
    public class TemplateManager : ITransient
    {
        public ITemplateHandler TemplateVar { get; set; }


        public string Replate(string templateStr, string templateVar, string tableName)
        {
          return  templateStr.Replace(templateVar, tableName);
        }
    }

}
