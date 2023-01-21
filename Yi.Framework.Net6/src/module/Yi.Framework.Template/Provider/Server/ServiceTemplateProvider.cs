using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstracts;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Server
{
    public class ServiceTemplateProvider : ProgramTemplateProvider
    {
        public ServiceTemplateProvider(string modelName, string entityName) : base(modelName, entityName)
        {
            BuildPath = $@"..\..\..\..\Yi.Framework.Service\{TemplateConst.ModelName}\{TemplateConst.EntityName}Service.cs";
            TemplatePath = $@"..\..\..\Template\Server\ServiceTemplate.txt";
        }
    }
}
