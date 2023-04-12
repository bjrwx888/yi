using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstract;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Server
{
    public class GetListOutputDtoTemplateProvider : ModelTemplateProvider
    {
        public GetListOutputDtoTemplateProvider(string modelName, string entityName, string nameSpaces) : base(modelName, entityName, nameSpaces)
        {
            BuildPath = $@"{TemplateConst.BuildRootPath}\{nameSpaces}.Application.Contracts\{TemplateConst.ModelName}\Dtos\{TemplateConst.EntityName}\{TemplateConst.EntityName}GetListOutputDto.cs";
            TemplatePath = $@"..\..\..\Template\Server\GetListOutputDtoTemplate.txt";
            EntityPath = $@"{TemplateConst.BuildEntityPath}\{nameSpaces}.Domain\{TemplateConst.ModelName}\Entities\{TemplateConst.EntityName}Entity.cs";
        }
    }
}
