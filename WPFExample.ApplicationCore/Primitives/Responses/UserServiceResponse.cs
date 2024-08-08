namespace WPFExample.ApplicationCore.Primitives.Responses;

public readonly record struct UserServiceResponse(bool IsSuccess, string Message);