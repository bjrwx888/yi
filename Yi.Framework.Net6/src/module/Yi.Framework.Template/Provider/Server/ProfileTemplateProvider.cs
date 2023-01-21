using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstract;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Server
{
    public class ProfileTemplateProvider : ProgramTemplateProvider
    {
        public ProfileTemplateProvider(string modelName, string entityName) : base(modelName, entityName)
        {
            BuildPath = $@"{TemplateConst.BuildRootPath}\Yi.Framework.Application\{TemplateConst.ModelName}\MapperConfig\{TemplateConst.EntityName}Profile.cs";
            TemplatePath = $@"..\..\..\Template\Server\ProfileTemplate.txt";
        }
    }
}
