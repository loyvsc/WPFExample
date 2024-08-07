using System.Net;
using System.Text;
using System.Text.Json;
using OneOf;
using WPFExample.ApplicationCore.Primitives.Interfaces;
using WPFExample.ApplicationCore.Primitives.Models;

namespace WPFExample.DAL.Services;

public class UserService : IUserService
{
    private readonly User _user;

    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient, User user)
    {
        _httpClient = httpClient;
        _user = user;
    }

    public async Task<string> RegisterAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("user", content, cancellationToken);

            return response.IsSuccessStatusCode ? "Registration complete" : "Some ultra rare error has occured";
        }
        catch (OperationCanceledException)
        {
            return "Operation canceled";
        }
        catch (HttpRequestException)
        {
            return "Network is unstable, try again later";
        }
        catch (Exception)
        {
            return "Some ultra rare error has occured";
        }
    }

    public async Task<string> Login(string login, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParameters = System.Web.HttpUtility.ParseQueryString(string.Empty);
            
            queryParameters.Add("username", login);
            queryParameters.Add("password", password);

            var response = await _httpClient.PostAsync($"user?{queryParameters}", null, cancellationToken);

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                int index = responseString.IndexOf(':');
                if (index != -1)
                {
                    string token = responseString.Substring(index + 1);
                    if (response.Headers.TryGetValues("x-expires-after", out var headerValues))
                    {
                        var expiresAfter = headerValues.FirstOrDefault();
                        if (expiresAfter != null)
                        {
                            _user.TokenExpiresIn = Convert.ToDateTime(expiresAfter);
                            _user.Token = token;
                            return "Ok";
                        }
                    }
                }
            }
            return "Some ultra rare error has occured";
        }
        catch (OperationCanceledException)
        {
            return "Operation canceled";
        }
        catch (HttpRequestException)
        {
            return "Network is unstable, try again later";
        }
        catch (Exception)
        {
            return "Some ultra rare error has occured";
        }
    }

    public async Task<string> Logout()
    {
        try
        {
            var response = await _httpClient.PostAsync("user/logout", null);

            return response.IsSuccessStatusCode ? "Ok" : "Some ultra rare error has occured";
        }
        catch (HttpRequestException)
        {
            return "Network is unstable, try again later";
        }
        catch (Exception)
        {
            return "Some ultra rare error has occured";
        }
    }

    public async Task<OneOf<User,string>> GetUser(string username, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsync("user/" + username, null, cancellationToken);
            
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => JsonSerializer.Deserialize<User>(responseString),
                HttpStatusCode.BadRequest or HttpStatusCode.NotFound => responseString,
                _ => "Some ultra rare error has occured"
            };
        }
        catch (OperationCanceledException)
        {
            return "Operation canceled";
        }
        catch (HttpRequestException)
        {
            return "Network is unstable, try again later";
        }
        catch (Exception)
        {
            return "Some ultra rare error has occured";
        }
    }

    //API does not allow this to be done
    public async Task RefreshToken()
    {
        
    }
}