using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp1.Common;
using JKToolKit.Spectre.AutoCompletion.Completion;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace ConsoleApp1;

public abstract class Program
{
    public static async Task<int> Main(string[] args)
    {
        CommandApp? app = null;

        var host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Information);
            })
            .ConfigureServices((context, services) =>
            {
                RegisterCommandModules(services);

                var registrar = new TypeRegistrar(services);
                app = new CommandApp(registrar);

                var provider = services.BuildServiceProvider();

                app.Configure(config =>
                {
                    config.SetApplicationName("ConsoleApp1");
                    
                    var modules = provider.GetServices<ICommandModule>();
                    foreach (var module in modules)
                    {
                        module.Configure(config);
                    }
                });
            })
            .Build();

        return app != null ? await app.RunAsync(args) : 1;
    }

    private static void RegisterCommandModules(IServiceCollection services)
    {
        var moduleType = typeof(ICommandModule);
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var moduleTypes = assemblies
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch { return []; }
            })
            .Where(t => moduleType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var type in moduleTypes)
        {
            services.AddSingleton(type);
            services.AddSingleton(typeof(ICommandModule), type);
        }
    }
}
