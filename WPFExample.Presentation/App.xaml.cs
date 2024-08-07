using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPFExample.Presentation.Extensions;
using WPFExample.Presentation.Views;

namespace WPFExample.Presentation;

public partial class App : Application
{
    private readonly IHost _host;
    
    public App()
    {
        _host = CreateHostBuilder().Build();
        _host.Start();
    }

    private IHostBuilder CreateHostBuilder()
    {
        return new HostBuilder()
            .AddHttpUtilities()
            .AddServices()
            .AddViewModels()
            .AddViews();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        _host.Start();

        var window = _host.Services.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }
}