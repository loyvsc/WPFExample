using System.Windows;
using WPFExample.Presentation.ViewModels;

namespace WPFExample.Presentation.Controls;

public partial class NavigationControl
{
    public NavigationControl()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel),typeof(ViewModelBase),typeof(NavigationControl),new PropertyMetadata(null));

    public ViewModelBase ViewModel
    {
        get => (ViewModelBase)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
}