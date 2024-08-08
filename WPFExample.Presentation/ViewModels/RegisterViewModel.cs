using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using WPFExample.ApplicationCore.Primitives.Interfaces;
using WPFExample.ApplicationCore.Primitives.Models;
using WPFExample.Presentation.Messanging;
using WPFExample.Presentation.Validators;
using WPFExample.Presentation.Views;

namespace WPFExample.Presentation.ViewModels;

public class RegisterViewModel : ViewModelBase, IDataErrorInfo
{
    public CloseDialogDelegate CloseDialogCallback;
    
    public string Email
    {
        get => _email;
        set => SetField(ref _email, value);
    }
    
    public string Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }
    
    public string PhoneNumber
    {
        get => _phoneNumber;
        set => SetField(ref _phoneNumber, value);
    }
    
    public string FirstName
    {
        get => _firstName;
        set => SetField(ref _firstName, value);
    }
    
    public string Lastname
    {
        get => _lastName;
        set => SetField(ref _lastName, value);
    }
    
    public string Username
    {
        get => _username;
        set => SetField(ref _username, value);
    }
    
    public ICommand RegisterCommand { get; }
    
    private string _email;
    private string _password;
    private string _phoneNumber;
    private string _firstName;
    private string _lastName;
    private string _username;

    private readonly IUserService _userService;
    private readonly IEventAggregator _eventAggregator;
    
    public RegisterViewModel(ILogger<RegisterViewModel> logger, 
        IEventAggregator eventAggregator, 
        IUserService userService) : base(logger)
    {
        _userService = userService;
        _eventAggregator = eventAggregator;
        RegisterCommand = new AsyncRelayCommand( async _ =>
        {
            var user = new User(0, _username, _firstName, _lastName, _email, _password, _phoneNumber, 0);
            var registerResult = await _userService.RegisterAsync(user);
            if (registerResult.IsSuccess)
            {
                _eventAggregator.GetEvent<RegisterResultPubSubEvent>().Publish(registerResult);
                Application.Current.Dispatcher.InvokeAsync(()=>CloseDialogCallback?.Invoke(true));
            }
        },_=>_validator.Validate(this).IsValid);
        
        _logger.LogInformation("RegisterViewModel initialized");
    }
    
    #region Validation
    
    private readonly RegisterValidator _validator = new();

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