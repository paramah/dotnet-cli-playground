namespace ConsoleApp1.Common;

using Spectre.Console.Cli;

public interface ICommandModule
{
    void Configure(IConfigurator config);
}
