using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.CommandLineUtils;
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


        public string Command => "new";
        public string? Description => "创建一个模板";

        public void CommandLineApplication(CommandLineApplication application)
        {
            var templateTypeOption = application.Option("-t|--moduleType", "模板类型:`module`|`porject`",
                CommandOptionType.SingleValue);
            var pathOption = application.Option("-p|--path", "创建路径", CommandOptionType.SingleValue);
            var csfOption = application.Option("-csf", "是否创建解决方案文件夹", CommandOptionType.NoValue);
            var moduleNameArgument = application.Argument("moduleName", "模块名", (_) => { });


            application.OnExecute(() =>
            {
                #region 处理生成类型

                var id = Guid.NewGuid().ToString("N");
                var zipPath = string.Empty;
                byte[] fileByteArray;

                var templateType = templateTypeOption.HasValue() ? templateTypeOption.Value() : "module";
                if (templateType == "module")
                {
                    //代表模块生成
                    fileByteArray = (_templateGenService.CreateModuleAsync(new TemplateGenCreateInputDto
                    {
                        Name = moduleNameArgument.Value,
                    }).Result);
                }
                else
                {
                    //代表模块生成
                    fileByteArray = _templateGenService.CreateProjectAsync(new TemplateGenCreateInputDto
                    {
                        Name = moduleNameArgument.Value,
                    }).Result;
                }

                var path = string.Empty;
                if (pathOption.HasValue())
                {
                    path = pathOption.Value();
                }

                zipPath = Path.Combine(path, $"{id}.zip");
                File.WriteAllBytes(zipPath, fileByteArray);

                #endregion

                #region 处理解决方案文件夹

                //默认是当前目录
                var unzipDirPath = "./";
                //如果创建解决方案文件夹
                if (csfOption.HasValue())
                {
                    var moduleName = moduleNameArgument.Value.ToLower().Replace(".", "-");

                    unzipDirPath = Path.Combine(path, moduleName);
                    if (Directory.Exists(unzipDirPath))
                    {
                        throw new UserFriendlyException($"文件夹[{unzipDirPath}]已存在，请删除后重试");
                    }

                    Directory.CreateDirectory(unzipDirPath);
                }

                #endregion

        
                ZipFile.ExtractToDirectory(zipPath, unzipDirPath);
                //创建压缩包后删除临时目录
                File.Delete(zipPath);

                Console.WriteLine("恭喜~模块已生成！");
                return 0;
            });
        }
    }
}