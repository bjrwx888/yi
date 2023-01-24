﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstract;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Server
{
    public class UpdateInputVoTemplateProvider : ModelTemplateProvider
    {
        public UpdateInputVoTemplateProvider(string modelName, string entityName) : base(modelName, entityName)
        {
            BuildPath = $@"{TemplateConst.BuildRootPath}\Yi.Framework.Application.Contracts\{TemplateConst.ModelName}\Dtos\{TemplateConst.EntityName}UpdateInputVo.cs";
            TemplatePath = $@"..\..\..\Template\Server\UpdateInputVoTemplate.txt";
            EntityPath = $@"{TemplateConst.BuildEntityPath}\Yi.Framework.Domain\{TemplateConst.ModelName}\Entities\{TemplateConst.EntityName}Entity.cs";
        }
    }
}