namespace ConsoleApp1.Commands.Hello;

using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

public class DwaSettings : CommandSettings
{
    [CommandOption("--shout")] 
    [Description("Czy wykrzyczeć powitanie")] 
    public bool Shout { get; set; }

    [CommandOption("--name <NAME>")]
    [Description("Imię adresata")]
    public string Name { get; set; } = "Gość";
}

public class DwaCommand : Command<DwaSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] DwaSettings settings)
    {
        var message = $"Hello Dwa: {settings.Name}";
        if (settings.Shout)
        {
            message = message.ToUpperInvariant();
        }

        Console.WriteLine(message);
        return 0;
    }
}