using ConsoleApp1.Common;
using Spectre.Console.Cli;

namespace ConsoleApp1.Commands.Hello;

public class Module : ICommandModule
{
    public void Configure(IConfigurator config)
    {
        config.AddBranch("hello", hello =>
        {
            hello.SetDescription("Zbór powitań");

            hello.AddCommand<JedenCommand>("jeden")
                .WithDescription("Wariant jeden");

            hello.AddCommand<DwaCommand>("dwa")
                .WithDescription("Wariant dwa");
        });
    }
}