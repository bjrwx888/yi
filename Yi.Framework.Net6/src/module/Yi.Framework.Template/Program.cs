using Yi.Framework.Core.Helper;
using Yi.Framework.Template;
using Yi.Framework.Template.Provider.Server;
using Yi.Framework.Template.Provider.Site;

TemplateFactory templateFactory = new();

//选择需要生成的模板提供者

string modelName = "School";
List<string> entityNames = new() { "Student" };

foreach (var entityName in entityNames)
{
    templateFactory.CreateTemplateProviders((option) =>
    {
        option.Add(new ServiceTemplateProvider(modelName, entityName));
        option.Add(new IServiceTemplateProvider(modelName, entityName));

        option.Add(new CreateInputVoTemplateProvider(modelName, entityName));
        option.Add(new UpdateInputVoTemplateProvider(modelName, entityName));
        option.Add(new GetListInputVoTemplateProvider(modelName, entityName));
        option.Add(new GetListOutputDtoTemplateProvider(modelName, entityName));
        option.Add(new GetOutputDtoTemplateProvider(modelName, entityName));

        option.Add(new ConstTemplateProvider(modelName, entityName));
        option.Add(new ProfileTemplateProvider(modelName, entityName));
        //option.Add(new ApiTemplateProvider(modelName, entityName));
    });
    //开始构建模板
    templateFactory.BuildTemplate();
    Console.WriteLine($"Yi.Framework.Template:{entityName}构建完成！");
}

Console.WriteLine("Yi.Framework.Template:模板全部生成完成！");
Console.ReadKey();

//根据模板文件生成项目文件
//var template = "";
//FileHelper.AllInfoReplace(template, "Template","BBS");