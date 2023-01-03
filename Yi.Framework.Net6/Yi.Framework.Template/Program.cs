using Yi.Framework.Template;
using Yi.Framework.Template.Provider;

TemplateFactory templateFactory = new();

//选择需要生成的模板提供者

string modelName = "ERP";
string entityName = "Test";

templateFactory.CreateTemplateProviders((option) =>
{
    option.Add(new ServceTemplateProvider(modelName, entityName));

});

//开始构建模板
templateFactory.BuildTemplate();
Console.WriteLine("Yi.Framework.Template模板生成完成！");
Console.ReadKey();