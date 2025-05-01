namespace AuthService.Models;

public class RevokedToken
{
    public int Id { get; set; }
    public string JwtId { get; set; } = string.Empty;
    public DateTime RevocationDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}