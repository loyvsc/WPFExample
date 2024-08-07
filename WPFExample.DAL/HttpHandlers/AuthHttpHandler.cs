using WPFExample.ApplicationCore.Primitives.Models;

namespace WPFExample.DAL.HttpHandlers;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly User _user;

    public AuthHeaderHandler(User user)
    {
        _user = user;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("Authorization", _user.Token);

        return await base.SendAsync(request, cancellationToken);
    }
}