using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstract;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Server
{
    public class GetListInputVoTemplateProvider : ModelTemplateProvider
    {
        public GetListInputVoTemplateProvider(string modelName, string entityName, string nameSpaces) : base(modelName, entityName, nameSpaces)
        {
            BuildPath = $@"{TemplateConst.BuildRootPath}\{nameSpaces}.Application.Contracts\{TemplateConst.ModelName}\Dtos\{TemplateConst.EntityName}\{TemplateConst.EntityName}GetListInputVo.cs";
            TemplatePath = $@"..\..\..\Template\Server\GetListInputVoTemplate.txt";
            EntityPath = $@"{TemplateConst.BuildEntityPath}\{nameSpaces}.Domain\{TemplateConst.ModelName}\Entities\{TemplateConst.EntityName}Entity.cs";
        }
    }
}
