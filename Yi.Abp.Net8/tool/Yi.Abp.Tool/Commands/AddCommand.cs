using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Abp.Tool.Commands
{
    public class AddCommand : ICommand
    {
        public List<string> CommandStrs => new List<string> { "add" };

        public Task InvokerAsync(Dictionary<string, string> options, string[] args)
        {
            //只有一个add
            if (args.Length <= 1)
            {
                throw new UserFriendlyException("命令错误，add命令后必须添加 类型");
            }

            //需要添加名称
            var addName = args[1];
            options.TryGetValue("modulePath", out var modulePath);
            options.TryGetValue("moduleName", out var moduleName);


            //添加的为模块
            if (addName == "module")
            {
                if (string.IsNullOrEmpty(modulePath) || string.IsNullOrEmpty(moduleName))
                {
                    throw new UserFriendlyException("命令错误，添加模块，必须指定模块路径及模块名称");
                }
                GetFirstSlnPath();
                var dotnetSlnCommandPart1 = $@"dotnet sln add {modulePath}\{moduleName}.";
                var dotnetSlnCommandPart2 = new List<string>() { "Application", "Application.Contracts", "Domain", "Domain.Shared", "SqlSugarCore" };
                var paths = dotnetSlnCommandPart2.Select(x => $@"{modulePath}\{moduleName}." + x).ToArray();
                CheckPathExist(paths);

                var cmdCommands = dotnetSlnCommandPart2.Select(x => dotnetSlnCommandPart1 + x).ToArray();
                StartCmd(cmdCommands);

            }
            else
            {
                throw new UserFriendlyException("命令暂时只支持模块添加，请尝试使用 module");
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取一个sln解决方案，多个将报错
        /// </summary>
        /// <returns></returns>
        private string GetFirstSlnPath()
        {
            string[] slnFiles = Directory.GetFiles("./", "*.sln");
            if (slnFiles.Length > 1)
            {
                throw new UserFriendlyException("当前目录包含多个sln解决方案，请只保留一个");
            }
            if (slnFiles.Length == 0)
            {
                throw new UserFriendlyException("当前目录未找到sln解决方案，请检查");
            }

            return slnFiles[0];
        }


        /// <summary>
        /// 执行cmd命令
        /// </summary>
        /// <param name="cmdCommands"></param>
        private void StartCmd(params string[] cmdCommands)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {string.Join("&", cmdCommands)}",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process proc = new Process
            {
                StartInfo = psi
            };

            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            Console.WriteLine(output);

            proc.WaitForExit();
        }

        /// <summary>
        /// 检查路径
        /// </summary>
        /// <param name="paths"></param>
        /// <exception cref="UserFriendlyException"></exception>
        private void CheckPathExist(string[] paths)
        {
            foreach (string path in paths)
            {
                if (!Directory.Exists(path))
                {
                    throw new UserFriendlyException($"路径错误，请检查你的路径，找不到：{path}");
                }
            }
        }
    }
}
