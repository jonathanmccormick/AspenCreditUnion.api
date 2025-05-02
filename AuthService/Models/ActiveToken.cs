namespace AuthService.Models;

public class ActiveToken
{
    public int Id { get; set; }
    public string JwtId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public string? DeviceInfo { get; set; }
    public string? IpAddress { get; set; }
}