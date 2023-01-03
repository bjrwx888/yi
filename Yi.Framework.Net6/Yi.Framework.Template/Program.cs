using Yi.Framework.Template;
using Yi.Framework.Template.Provider;

TemplateFactory templateFactory = new();

//选择需要生成的模板提供者
templateFactory.CreateTemplateProviders((option) =>
{
    option.Add(new ServceTemplateProvider());

});

//开始构建模板
templateFactory.BuildTemplate();