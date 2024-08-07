using Microsoft.Extensions.Logging;

namespace WPFExample.Presentation.ViewModels;

public abstract class ViewModelBase : NotifyPropertyChangedBase
{
    private readonly ILogger _logger;

    protected ViewModelBase(ILogger logger)
    {
        _logger = logger;
    }
}