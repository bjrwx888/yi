using Yi.Framework.Core.Helper;
using Yi.Framework.Template;
using Yi.Framework.Template.Provider.Server;
using Yi.Framework.Template.Provider.Site;

TemplateFactory templateFactory = new();

//选择需要生成的模板提供者

string modelName = "School";
string nameSpaces = "Yi.BBS";
List<string> entityNames = new() { "Student" };

foreach (var entityName in entityNames)
{
    templateFactory.CreateTemplateProviders((option) =>
    {
        option.Add(new ServiceTemplateProvider(modelName, entityName, nameSpaces));
        option.Add(new IServiceTemplateProvider(modelName, entityName, nameSpaces));

        option.Add(new CreateInputVoTemplateProvider(modelName, entityName, nameSpaces));
        option.Add(new UpdateInputVoTemplateProvider(modelName, entityName, nameSpaces));
        option.Add(new GetListInputVoTemplateProvider(modelName, entityName, nameSpaces));
        option.Add(new GetListOutputDtoTemplateProvider(modelName, entityName, nameSpaces));
        option.Add(new GetOutputDtoTemplateProvider(modelName, entityName, nameSpaces));

        option.Add(new ConstTemplateProvider(modelName, entityName, nameSpaces));
        option.Add(new ProfileTemplateProvider(modelName, entityName, nameSpaces));
        //option.Add(new ApiTemplateProvider(modelName, entityName));
    });
    //开始构建模板
    //templateFactory.BuildTemplate();
    Console.WriteLine($"Yi.Framework.Template:{entityName}构建完成！");
}

Console.WriteLine("Yi.Framework.Template:模板全部生成完成！");
Console.ReadKey();

//根据模板文件生成项目文件
//var template = "D:\\C#\\Yi\\Yi.Framework.Net6\\src\\project\\BBS";
//FileHelper.AllInfoReplace(template, "Template", "BBS");