using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yi.Abp.Tool.Application.Contracts;
using Yi.Abp.Tool.Application.Contracts.Dtos;

namespace Yi.Abp.Tool.Commands
{
    public class NewCommand : ICommand
    {
        private readonly ITemplateGenService _templateGenService;
        public NewCommand(ITemplateGenService templateGenService)
        {
            _templateGenService = templateGenService;
        }

        public List<string> CommandStrs => new List<string>() { "new" };


        public async Task InvokerAsync(Dictionary<string, string> options, string[] args)
        {
            //只有一个new
            if (args.Length <= 1)
            {
                throw new UserFriendlyException("命令错误，new命令后必须添加 名称");
            }
            string name = args[1];

            options.TryGetValue("t", out var templateType);

            if (templateType == "module")
            {
                //代表模块生成
                var fileResult = await _templateGenService.CreateModuleAsync(new TemplateGenCreateInputDto
                {
                    Name = name,
                });
                var fileContent = fileResult as FileContentResult;
                File.WriteAllText("./", Encoding.UTF8.GetString(fileContent.FileContents));

            }
            else
            {
                //暂未实现
                throw new NotImplementedException();
                //代表模块生成
                var fileResult = await _templateGenService.CreateProjectAsync(new TemplateGenCreateInputDto
                {
                    Name = name,
                });
            }



        }
    }
}
