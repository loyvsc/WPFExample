using Microsoft.Extensions.Logging;

namespace WPFExample.Presentation.ViewModels;

public class AboutDeveloperViewModel : ViewModelBase
{
    public AboutDeveloperViewModel(ILogger<AboutDeveloperViewModel> logger) : base(logger)
    { 
        _logger.LogInformation("AboutDeveloperViewModel initialized");
    }
}