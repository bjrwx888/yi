using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstract;
using Yi.Framework.Template.Const;

namespace Yi.Framework.Template.Provider
{
    public class ServceTemplateProvider : ProgramTemplateProvider
    {
        public ServceTemplateProvider(string modelName, string entityName) : base( modelName,entityName)
        {
            BuildPath = $@"D:\CC.Yi\CC.Yi\Yi.Framework.Net6\Yi.Framework.Template\Code\{TemplateConst.EntityName}Entity.cs";
            TemplatePath = $@"D:\CC.Yi\CC.Yi\Yi.Framework.Net6\Yi.Framework.Template\Template\ServiceTemplate.txt";
            AddTemplateDic("Yi.Framework", "Yi.Test");
        }
    }
}
