using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WPFExample.ApplicationCore.Primitives.Interfaces;
using WPFExample.DAL.HttpHandlers;
using WPFExample.DAL.Services;
using WPFExample.Presentation.ViewModels;
using WPFExample.Presentation.Views;

namespace WPFExample.Presentation.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder host)
    {
        return host.ConfigureServices(collection =>
        {
            collection.AddSingleton<MainWindowViewModel>();
            collection.AddSingleton<AuthViewModel>();
            collection.AddSingleton<RegisterViewModel>();
            collection.AddSingleton<AboutDeveloperViewModel>();
            collection.AddSingleton<UserInformationViewModel>();
        });
    }
    
    public static IHostBuilder AddViews(this IHostBuilder host)
    {
        return host.ConfigureServices(collection =>
        {
            collection.AddSingleton<MainWindow>(x=>new MainWindow()
            {
                DataContext = x.GetRequiredService<MainWindowViewModel>()
            });
            collection.AddScoped<RegisterView>(x => new RegisterView()
            {
                DataContext = x.GetRequiredService<RegisterViewModel>()
            });
        });
    }
    
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        return host.ConfigureServices(collection =>
        {
            collection.AddSingleton<IUserService, UserService>();
        });
    }
    
    public static IHostBuilder AddHttpUtilities(this IHostBuilder host)
    {
        return host.ConfigureServices(collection =>
        {
            collection.AddHttpClient<IUserService, UserService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://petstore.swagger.io/v2/"))
                .AddHttpMessageHandler<AuthHeaderHandler>();

            collection.AddSingleton<AuthHeaderHandler>();
        });
    }

    public static IHostBuilder AddSerilog(this IHostBuilder host)
    {
        return host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration);
        });
    }

    public static IHostBuilder AddConfiguration(this IHostBuilder host)
    {
        return 
            host.ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            });
    }
}