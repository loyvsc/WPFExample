using OneOf;
using WPFExample.ApplicationCore.Primitives.Models;

namespace WPFExample.ApplicationCore.Primitives.Interfaces;

public interface IUserService
{
    public Task<string> RegisterAsync(User user, CancellationToken cancellationToken = default);

    public Task<string> Login(string login, string password, CancellationToken cancellationToken = default);

    public Task<string> Logout();

    public Task<OneOf<User,string>> GetUser(string username, CancellationToken cancellationToken = default);

    public Task RefreshToken();
}