using Microsoft.Extensions.Logging;
using WPFExample.ApplicationCore.Primitives.Interfaces;
using WPFExample.ApplicationCore.Primitives.Models;
using WPFExample.Presentation.Messanging;

namespace WPFExample.Presentation.ViewModels;

public class UserInformationViewModel : ViewModelBase
{
    public bool LoggedIn
    {
        get => _loggedIn;
        set => SetField(ref _loggedIn, value);
    }

    public User User
    {
        get => _user;
        set => SetField(ref _user, value);
    }
    
    private bool _loggedIn;
    private User _user;
    private readonly IEventAggregator _eventAggregator;
    private readonly IUserService _userService;
    
    public UserInformationViewModel(ILogger<UserInformationViewModel> logger,
        IUserService userService,
        IEventAggregator eventAggregator) : base(logger)
    {
        _eventAggregator = eventAggregator;
        _userService = userService;
        _eventAggregator.GetEvent<LoginExecutedPubSubEvent>().Subscribe(OnLoginExecuted);
        _logger.LogInformation("UserInformationViewModel initialized");
    }

    private async void OnLoginExecuted((bool value, User user) tuple)
    {
        LoggedIn = tuple.value;
        var result = await _userService.GetUser(tuple.user.Username);
        if (result.IsT0)
        {
            User = result.AsT0;
        }
    }
}