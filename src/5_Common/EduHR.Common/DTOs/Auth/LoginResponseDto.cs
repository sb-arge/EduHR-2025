namespace EduHR.Common.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    // İhtiyaç duyulursa, kullanıcının rolleri veya yetkileri de eklenebilir.
}