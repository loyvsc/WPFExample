using System.Windows.Input;
using Microsoft.Extensions.Logging;

namespace WPFExample.Presentation.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ViewModelBase AuthViewModel
    {
        get => _authViewModel;
        set => SetField(ref _authViewModel, value);
    }
    
    public int CurrentViewmodelIndex
    {
        get => _currentViewmodelIndex;
        set
        {
            SetField(ref _currentViewmodelIndex, value);
            OnPropertyChanged(nameof(CurrentPageViewModel));
        }
    }
    
    public ViewModelBase CurrentPageViewModel => ViewModels[CurrentViewmodelIndex];
    
    public ICommand NextPage { get; }
    public ICommand PreviousPage { get; }

    public List<ViewModelBase> ViewModels { get; }

    private int _currentViewmodelIndex;
    private ViewModelBase _authViewModel;
    
    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, 
        AuthViewModel authViewModel, 
        UserInformationViewModel userInformationViewModel,
        AboutDeveloperViewModel aboutDeveloperViewModel) : base(logger)
    {
        AuthViewModel = authViewModel;
        ViewModels = [userInformationViewModel, aboutDeveloperViewModel];
        
        NextPage = new AsyncRelayCommand(_ =>
        {
            CurrentViewmodelIndex++;
        },_ => CurrentViewmodelIndex != ViewModels.Count - 1);
        PreviousPage = new AsyncRelayCommand(_ =>
        {
            CurrentViewmodelIndex--;
        },_ => CurrentViewmodelIndex != 0);
    }
}