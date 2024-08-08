using Microsoft.Extensions.Logging;

namespace WPFExample.Presentation.ViewModels;

public abstract class ViewModelBase : NotifyPropertyChangedBase
{
    protected readonly ILogger _logger;

    protected ViewModelBase(ILogger logger)
    {
        _logger = logger;
    }
}