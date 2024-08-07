using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPFExample.ApplicationCore.Primitives.Interfaces;
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
            collection.AddSingleton<MainWindow>();
        });
    }
    
    public static IHostBuilder AddServices(this IHostBuilder host)
    {
        return host.ConfigureServices(collection =>
        {
            collection.AddSingleton<IUserService, UserService>();
        });
    }
}