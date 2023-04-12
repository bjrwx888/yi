using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstract;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Server
{
    public class IServiceTemplateProvider : ProgramTemplateProvider
    {
        public IServiceTemplateProvider(string modelName, string entityName, string nameSpaces) : base(modelName, entityName, nameSpaces)
        {
            BuildPath = $@"{TemplateConst.BuildRootPath}\{nameSpaces}.Application.Contracts\{TemplateConst.ModelName}\I{TemplateConst.EntityName}Service.cs";
            TemplatePath = $@"..\..\..\Template\Server\IServiceTemplate.txt";
        }
    }
}
