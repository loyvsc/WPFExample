using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPFExample.Presentation.ViewModels;

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
}