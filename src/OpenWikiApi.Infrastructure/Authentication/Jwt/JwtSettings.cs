namespace OpenWikiApi.Infrastructure.Authentication.Jwt;

public class JwtSettings
{
    public const string TypeOfAuthentication = "Bearer";
    public const string SectionName = "JwtSettings";
    public string Secret { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
}