using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;
using Volo.Abp.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Yi.Abp.Tool
{
    public class CommandInvoker : ISingletonDependency
    {
        private readonly IEnumerable<ICommand> _commands;
        private CommandLineApplication Application { get; }

        public CommandInvoker(IEnumerable<ICommand> commands)
        {
            _commands = commands;
            Application = new CommandLineApplication();
            InitCommand();
        }

        private void InitCommand()
        {    
            Application.HelpOption("-h");
            
            foreach (var command in _commands)
            {
                Application.Command(command.Command, con => command.CommandLineApplicationAsync(con).Wait());
            }
        }

        public async Task InvokerAsync(string[] args)
        {
            //使用哪个命令，根据第一参数来判断，如果都不是，打印help
            // foreach (var commandLineApplication in Application.Commands)
            // {
            //     commandLineApplication.Execute(args);
            // }

            Application.Execute(args);
        }
    }
}