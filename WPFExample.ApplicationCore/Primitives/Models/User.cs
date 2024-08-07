using System.Text.Json.Serialization;

namespace WPFExample.ApplicationCore.Primitives.Models;

public class User
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("username")] public string Username { get; set; } = string.Empty;
    [JsonPropertyName("firstName")] public string Firstname { get; set; } = string.Empty;
    [JsonPropertyName("lastName")] public string Lastname { get; set; } = string.Empty;
    [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;
    [JsonPropertyName("password")] public string Password { get; set; } = string.Empty;
    [JsonPropertyName("phone")] public int Phone { get; set; }
    [JsonPropertyName("userStatus")] public int UserStatus { get; set; }
    
    [JsonIgnore] public string Token { get; set; } = string.Empty;
    [JsonIgnore] public DateTime TokenExpiresIn { get; set; } = DateTime.MinValue;
}