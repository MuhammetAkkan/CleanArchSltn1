using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clean.Api.JWT;

public static class JwtProvider
{
    private static string GetSecretKey()
    {
        // Bu anahtarı genellikle appsettings.json veya bir çevresel değişkenle saklayın
        return "my secret key my secret key my secret key";
    }

    public static string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: "Muhammet Akkan",
            audience: "www.MuhammetAkkan.com",
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetSecretKey())),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
