namespace EFCoreIdentity.Dtos;

public sealed record ChangePasswordUsingTokenDto(string Email, string NewPassword, string Token);

