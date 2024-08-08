using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace WPFExample.Presentation.Resources.Styles;

public partial class TextBoxStyles : ResourceDictionary
{
    public void HyperLinkRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        var processInfo = new ProcessStartInfo(e.Uri.ToString())
        {
            UseShellExecute = true
        };
        Process.Start(processInfo);
    }
}