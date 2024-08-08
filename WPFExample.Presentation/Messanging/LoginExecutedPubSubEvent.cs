using WPFExample.ApplicationCore.Primitives.Models;

namespace WPFExample.Presentation.Messanging;

public sealed class LoginExecutedPubSubEvent : PubSubEvent<(bool,User)>;