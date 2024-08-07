using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        });
    }
}