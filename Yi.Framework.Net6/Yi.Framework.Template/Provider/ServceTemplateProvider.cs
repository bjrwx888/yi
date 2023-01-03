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
            BuildPath = $@"..\..\..\Code\Yi.Framework.Service\{TemplateConst.ModelName}\{TemplateConst.EntityName}Service.cs";
            TemplatePath = $@"..\..\..\Template\ServiceTemplate.txt";
        }
    }
}
