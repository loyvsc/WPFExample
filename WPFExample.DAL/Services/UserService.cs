using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using OneOf;
using WPFExample.ApplicationCore.Primitives.Interfaces;
using WPFExample.ApplicationCore.Primitives.Models;
using WPFExample.ApplicationCore.Primitives.Responses;

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

    public async Task<UserServiceResponse> RegisterAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("user", content, cancellationToken);

            return response.IsSuccessStatusCode ? new UserServiceResponse(true,"Registration complete") : new UserServiceResponse(false,"Some ultra rare error has occured");
        }
        catch (OperationCanceledException)
        {
            return new UserServiceResponse(false,"Operation canceled");
        }
        catch (HttpRequestException)
        {
            return new UserServiceResponse(false,"Network is unstable, try again later");
        }
        catch (Exception)
        {
            return new UserServiceResponse(false,"Some ultra rare error has occured");
        }
    }

    public async Task<UserServiceResponse> Login(string login, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            var queryParameters = System.Web.HttpUtility.ParseQueryString(string.Empty);
            
            queryParameters.Add("username", login);
            queryParameters.Add("password", password);

            string uri = $"user/login?{queryParameters}";
            var response = await _httpClient.GetAsync(uri, cancellationToken);

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                int index = responseString.LastIndexOf(':');
                if (index != -1)
                {
                    string token = responseString.Substring(index + 1);
                    token = token.Remove(token.Length - 2, 2);
                    if (response.Headers.TryGetValues("x-expires-after", out var headerValues))
                    {
                        var expiresAfter = headerValues.FirstOrDefault();
                        if (expiresAfter != null)
                        {
                            _user.TokenExpiresIn = DateTime.ParseExact(expiresAfter, "ddd MMM dd HH:mm:ss 'UTC' yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                            _user.Token = token;
                            _user.Username = login;
                            _user.Password = password;
                            return new UserServiceResponse(true,"Ok");
                        }
                    }
                }
            }
            return new UserServiceResponse(true,"Some ultra rare error has occured");
        }
        catch (OperationCanceledException)
        {
            return new UserServiceResponse(false, "Operation canceled");
        }
        catch (HttpRequestException)
        {
            return new UserServiceResponse(false, "Network is unstable, try again later");
        }
        catch (Exception)
        {
            return new UserServiceResponse(false, "Some ultra rare error has occured");
        }
    }

    public async Task<UserServiceResponse> Logout()
    {
        try
        {
            var response = await _httpClient.GetAsync("user/logout");
            if (response.IsSuccessStatusCode)
            {
                _user.TokenExpiresIn = DateTime.MinValue;
                _user.Token = string.Empty;
                _user.Firstname = string.Empty;
                _user.Lastname = string.Empty;
                _user.Email = string.Empty;
                _user.Username = string.Empty;
                _user.Password = string.Empty;
                _user.Phone = string.Empty;
                _user.UserStatus = 0;
                return new UserServiceResponse(true, "Ok");
            }

            return new UserServiceResponse(true, "Some ultra rare error has occured");
        }
        catch (HttpRequestException)
        {
            return new UserServiceResponse(false,"Network is unstable, try again later");
        }
        catch (Exception)
        {
            return new UserServiceResponse(false,"Some ultra rare error has occured");
        }
    }

    public async Task<OneOf<User,string>> GetUser(string username, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"user/{username}", cancellationToken);
            
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