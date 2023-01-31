using Calculator.Interfaces;
using Calculator.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator;

internal class Program
{
    public  static ServiceProvider Service;
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<IConsoleIo, ConsoleIo>();
        serviceCollection.AddSingleton<IValidator, Validator>();
        serviceCollection.AddSingleton<IFileService, FileService>();

        if (args.Length != 0)
        {
            Service = serviceCollection.AddSingleton<CalculatorManager>(
                    provider => new FileProvider(provider.GetService<IValidator>(),
                        provider.GetService<IFileService>(),
                        args[0]))
                .BuildServiceProvider();
        }
        else
        {
            Service = serviceCollection.AddSingleton<CalculatorManager>(
            provider => new ConsoleProvider(provider.GetService<IValidator>(),
                provider.GetService<IConsoleIo>()))

                .BuildServiceProvider();
        }

        CalculatorManager cm = Service.GetService<CalculatorManager>();
        
        cm.TemplateMethod();
    }        
}