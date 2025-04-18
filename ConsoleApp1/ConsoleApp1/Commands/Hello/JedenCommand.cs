namespace ConsoleApp1.Commands.Hello;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

public class JedenSettings : CommandSettings
{
    [CommandOption("--title <TITLE>")]
    [Description("Tytuł powitania")]
    public string Title { get; set; } = "Pan/Pani";

    [CommandOption("--name <NAME>")]
    [Description("Imię adresata")]
    public string Name { get; set; } = "Użytkownik";
}

public class JedenCommand : Command<JedenSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] JedenSettings settings)
    {
        Console.WriteLine($"Hello Jeden: {settings.Title} {settings.Name}!");
        return 0;
    }
}
