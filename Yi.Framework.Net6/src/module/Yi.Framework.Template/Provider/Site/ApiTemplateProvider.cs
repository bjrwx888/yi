﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstracts;
using Yi.Framework.Template.ConstClasses;

namespace Yi.Framework.Template.Provider.Site
{
    public class ApiTemplateProvider : ProgramTemplateProvider
    {
        public ApiTemplateProvider(string modelName, string entityName) : base(modelName, entityName)
        {
            BuildPath = $@"..\..\..\Code_Site\src\api\{TemplateConst.LowerModelName}\{TemplateConst.LowerEntityName}Api.js";
            TemplatePath = $@"..\..\..\Template\Site\ApiTemplate.txt";
        }
    }
}
