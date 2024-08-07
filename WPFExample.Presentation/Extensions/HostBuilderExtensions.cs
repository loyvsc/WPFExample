using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
}