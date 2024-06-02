using System;
using System.Collections.Generic;
using System.IO.Compression;
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
            var id = Guid.NewGuid().ToString("N");
            //只有一个new
            if (args.Length <= 1)
            {
                throw new UserFriendlyException("命令错误，new命令后必须添加 名称");
            }
            string name = args[1];

            options.TryGetValue("t", out var templateType);
            var zipPath = string.Empty;
            if (templateType == "module")
            {
                //代表模块生成
                var fileByteArray = await _templateGenService.CreateModuleAsync(new TemplateGenCreateInputDto
                {
                    Name = name,
                });
                zipPath = $"{id}.zip";
                await File.WriteAllBytesAsync(zipPath, fileByteArray);

            }
            else
            {
                //暂未实现
                throw new NotImplementedException("暂未实现");
                //代表模块生成
                var fileResult = await _templateGenService.CreateProjectAsync(new TemplateGenCreateInputDto
                {
                    Name = name,
                });
            }

            //默认是当前目录
            var unzipDirPath = "./";
            //如果创建解决方案文件夹
            if (templateType == "module"&&options.TryGetValue("csf", out _))
            {
                var moduleName = name.ToLower().Replace(".", "-");

                if (Directory.Exists(moduleName))
                {
                    throw new UserFriendlyException($"文件夹[{moduleName}]已存在，请删除后重试");
                }
                Directory.CreateDirectory(moduleName);
                unzipDirPath = moduleName;
            }
            ZipFile.ExtractToDirectory(zipPath, unzipDirPath);
            //创建压缩包后删除临时目录
            File.Delete(zipPath);


            await Console.Out.WriteLineAsync("模块已生成！");
        }
    }
}
