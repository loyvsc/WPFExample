using System.Windows;
using WPFExample.Presentation.ViewModels;

namespace WPFExample.Presentation.Views;

public delegate void CloseDialogDelegate(bool value);

public partial class RegisterView : Window
{
    public RegisterView()
    {
        InitializeComponent();
    }

    public RegisterView(RegisterViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.CloseDialogCallback += CloseDialogCallback;
    }

    private void CloseDialogCallback(bool value)
    {
        this.DialogResult = value;
    }
}