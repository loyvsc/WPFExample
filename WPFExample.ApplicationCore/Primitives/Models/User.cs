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
    [JsonPropertyName("phone")] public int Phone { get; set; }
    [JsonPropertyName("userStatus")] public int UserStatus { get; set; }
}