﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Abp.Tool.Commands
{
    public class HelpCommand : ICommand
    {
        public List<string> CommandStrs => new List<string> { "h", "help", "-h", "-help" };

        public Task InvokerAsync(Dictionary<string, string> options, string[] args)
        {
            string? errorMsg = null;
            if (options.TryGetValue("error", out _))
            {
                errorMsg = "您输入的命令有误，请检查，以下帮助命令提示：";
            }
            Console.WriteLine($"""
                {errorMsg}
                使用:

                    yi-abp <command> <target> [options]

                命令列表:
                
                    > v: 查看yi-abp工具版本号
                    > help: 查看帮助列表，写下命令` yi-abp help <command> `
                    > new: 创建模板--（正在更新）

                """);
            return Task.CompletedTask;
        }
    }
}