using WPFExample.Presentation.ViewModels;

namespace WPFExample.Presentation.State;

public abstract class StoreBase : NotifyPropertyChangedBase, IDisposable
{
    public virtual void Dispose(){}
}