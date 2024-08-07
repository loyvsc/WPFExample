using WPFExample.ApplicationCore.Primitives.Models;

namespace WPFExample.ApplicationCore.Primitives.Interfaces;

public interface IUserService
{
    public Task<string> CreateUserAsync(User user, CancellationToken cancellationToken = default);

    public Task Login(string login, string password);

    public Task Logout();

    public Task<User> GetUser(string username);
}