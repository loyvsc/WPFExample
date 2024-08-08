namespace WPFExample.Presentation.State;

public class MessageStore : StoreBase
{
    public string Message
    {
        get => _message;
        set => SetField(ref _message, value);
    }

    private string _message;

    public void ClearStore()
    {
        Message = string.Empty;
    }
}