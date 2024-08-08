using System.Text.Json.Serialization;

namespace WPFExample.ApplicationCore.Primitives.Models;

public class User
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; }
    [JsonPropertyName("firstName")] public string Firstname { get; set; }
    [JsonPropertyName("lastName")] public string Lastname { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
    [JsonPropertyName("password")] public string Password { get; set; }
    [JsonPropertyName("phone")] public string Phone { get; set; }
    [JsonPropertyName("userStatus")] public int UserStatus { get; set; }
    
    [JsonIgnore] public string Token { get; set; } = string.Empty;
    [JsonIgnore] public DateTime TokenExpiresIn { get; set; } = DateTime.MinValue;

    public User()
    {
    }

    public User(int id, string username, string firstname, string lastname, string email, string password, string phone, int userStatus)
    {
        Id = id;
        Username = username;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Password = password;
        Phone = phone;
        UserStatus = userStatus;
    }
}