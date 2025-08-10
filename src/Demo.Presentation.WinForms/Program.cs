using System;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Demo.Infrastructure; 
using Demo.Presentation.WinForms; 

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        using var host = CreateHostBuilder(args).Build();
        var form = host.Services.GetRequiredService<MigrationForm>();
        Application.Run(form);
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((ctx, services) =>
            {
                services.AddInfrastructure(ctx.Configuration);
                //services.AddLogging();
                services.AddScoped<MigrationForm>(); // WinForms resolved via DI
            });
}
