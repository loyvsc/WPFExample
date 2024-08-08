using OneOf;
using WPFExample.ApplicationCore.Primitives.Models;
using WPFExample.ApplicationCore.Primitives.Responses;

namespace WPFExample.ApplicationCore.Primitives.Interfaces;

public interface IUserService
{
    public Task<UserServiceResponse> RegisterAsync(User user, CancellationToken cancellationToken = default);

    public Task<UserServiceResponse> Login(string login, string password, CancellationToken cancellationToken = default);

    public Task<UserServiceResponse> Logout();

    public Task<OneOf<User,string>> GetUser(string username, CancellationToken cancellationToken = default);

    public Task RefreshToken();
}