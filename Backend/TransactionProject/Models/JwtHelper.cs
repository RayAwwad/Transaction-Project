using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtHelper
{
    private readonly IConfiguration configuration;

    public JwtHelper(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateToken(int userId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("Id", userId.ToString()),
           
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signin
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
