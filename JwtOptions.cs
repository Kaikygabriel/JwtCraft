namespace JwtCraft;

public static class JwtOptions
{
    public static string SecretKey { get; set; } = null!;
    public static string Audience { get; set; } = null!;
    public static string Issuer { get; set; } = null!;
    public static int?  TokenValidInMinutes { get; set; } 
}