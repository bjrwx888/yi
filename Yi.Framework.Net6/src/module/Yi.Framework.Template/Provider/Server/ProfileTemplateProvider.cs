using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstracts;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Server
{
    public class ProfileTemplateProvider : ProgramTemplateProvider
    {
        public ProfileTemplateProvider(string modelName, string entityName) : base(modelName, entityName)
        {
            BuildPath = $@"..\..\..\..\Yi.Framework.DtoModel\{TemplateConst.ModelName}\{TemplateConst.EntityName}\MapperConfig\{TemplateConst.EntityName}Profile.cs";
            TemplatePath = $@"..\..\..\Template\Server\ProfileTemplate.txt";
        }
    }
}
