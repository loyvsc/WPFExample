using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Serilog;
using WPFExample.ApplicationCore.Primitives.Interfaces;
using WPFExample.ApplicationCore.Primitives.Models;
using WPFExample.ApplicationCore.Primitives.Responses;
using WPFExample.Presentation.Messanging;
using WPFExample.Presentation.State;
using WPFExample.Presentation.Validators;
using WPFExample.Presentation.Views;

namespace WPFExample.Presentation.ViewModels;

public class AuthViewModel : ViewModelBase, IDataErrorInfo
{
    public string Login
    {
        get => _login;
        set
        {
            SetField(ref _login, value);
            LoginErrorStore.Message = string.Empty;
        }
    }
    
    public string Password
    {
        get => _password;
        set
        {
            SetField(ref _password, value);
            LoginErrorStore.Message = string.Empty;
        }
    }
    
    public ICommand AuthCommand { get; }
    public ICommand RegisterCommand { get; }
    public ICommand LogoutCommand { get; }

    public bool IsLogin
    {
        get => _isLogin;
        set => SetField(ref _isLogin, value);
    }
    
    public MessageStore LoginErrorStore { get; }

    private bool _isLogin;
    private string _login;
    private string _password;
    private readonly User _user;
    private readonly IUserService _userService;
    private readonly RegisterViewModel _registerViewModel;
    private readonly IEventAggregator _eventAggregator;
    
    public AuthViewModel(ILogger<AuthViewModel> logger, 
        User user,
        IUserService userService,
        IEventAggregator eventAggregator, 
        RegisterViewModel registerViewModel) : base(logger)
    {
        _user = user;
        _userService = userService;
        _eventAggregator = eventAggregator;
        _registerViewModel = registerViewModel;

        LoginErrorStore = new MessageStore();

        AuthCommand = new AsyncRelayCommand(async x =>
        {
            if (Login != string.Empty && Password != string.Empty)
            {
                var authResult = await _userService.Login(Login, Password);
                IsLogin = authResult.IsSuccess;
                if (!IsLogin)
                {
                    LoginErrorStore.Message = authResult.Message;
                }
                else
                {
                    _eventAggregator.GetEvent<LoginExecutedPubSubEvent>().Publish((true,_user));
                }
            }
        },_=>_validator.Validate(this).IsValid);

        RegisterCommand = new RelayCommand( x =>
        {
            var window = new RegisterView(_registerViewModel);
            _eventAggregator.GetEvent<RegisterResultPubSubEvent>().Subscribe(OnRegisterResultEvent);

            window.ShowDialog();
        });

        LogoutCommand = new AsyncRelayCommand(async _ =>
        {
            try
            {
                await _userService.Logout();
                _eventAggregator.GetEvent<LoginExecutedPubSubEvent>().Publish((false,_user));
                IsLogin = false;
            }
            catch (Exception ex)
            {
                LoginErrorStore.Message = ex.Message;
            }
        },_=>_isLogin);
    }

    private void OnRegisterResultEvent(UserServiceResponse obj)
    {
        Login = _user.Username;
        Password = _user.Password;
        var authResult = _userService.Login(Login, Password).GetAwaiter().GetResult();
        IsLogin = authResult.IsSuccess;
        if (!IsLogin)
        {
            LoginErrorStore.Message = authResult.Message;
        }
        else
        {
            _eventAggregator.GetEvent<LoginExecutedPubSubEvent>().Publish((true,_user));
        }
        _eventAggregator.GetEvent<RegisterResultPubSubEvent>().Unsubscribe(OnRegisterResultEvent);
    }

    #region Validation
    
    private readonly AuthValidator _validator = new();

    public string this[string columnName]
    {
        get
        {
            var firstOrDefault = _validator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
            return firstOrDefault != null ? firstOrDefault.ErrorMessage : "";
        }
    }

    public string Error
    {
        get
        {
            var results = _validator.Validate(this);
            if (results != null && results.Errors.Count != 0)
            {
                return string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
            }

            return string.Empty;
        }
    }

    #endregion
}