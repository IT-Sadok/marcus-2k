namespace ASP_NET.Classes;

public class Options : IOptions
{
    public string JwtKey { get; } = string.Empty;
    public string JwtIssuer { get; } = string.Empty;
    public string JwtAudience { get; } = string.Empty;

    public Options(IConfiguration configuration)
    {
        this.JwtKey = configuration["Jwt:Key"];
        this.JwtIssuer = configuration["Jwt:Issuer"];
        this.JwtAudience = configuration["Jwt:Audience"];
    }
}

public interface IOptions
{

    public string JwtKey { get; }
    public string JwtIssuer { get; }
    public string JwtAudience { get; }
}
